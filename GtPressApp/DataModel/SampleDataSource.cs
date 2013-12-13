using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
// The data model defined by this file serves as a representative example of a strongly-typed
// model that supports notification when members are added, removed, or modified.  The property
// names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs.
using GtPress.StoreApp.Common;

namespace GtPress.StoreApp.DataModel
{
    /// <summary>
    /// Base class for <see cref="SampleDataItem"/> and <see cref="SampleDataGroup"/> that
    /// defines properties common to both.
    /// </summary>
    [Windows.Foundation.Metadata.WebHostHidden]
    public abstract class SampleDataCommon : BindableBase
    {
        private static Uri _baseUri = new Uri("ms-appx:///");

        public SampleDataCommon(String uniqueId, String title, String subtitle, String imagePath, String description)
        {
            this._uniqueId = uniqueId;
            this._title = title;
            this._subtitle = subtitle;
            this._description = description;
            this._imagePath = imagePath;
        }

        private string _uniqueId = string.Empty;
        public string UniqueId
        {
            get { return this._uniqueId; }
            set { this.SetProperty(ref this._uniqueId, value); }
        }

        private string _title = string.Empty;
        public string Title
        {
            get { return this._title; }
            set { this.SetProperty(ref this._title, value); }
        }

        private string _subtitle = string.Empty;
        public string Subtitle
        {
            get { return this._subtitle; }
            set { this.SetProperty(ref this._subtitle, value); }
        }

        private string _description = string.Empty;
        public string Description
        {
            get { return this._description; }
            set { SetProperty(ref _description, value); }
        }

        private ImageSource _image = null;
        private String _imagePath = null;
        public ImageSource Image
        {
            get
            {
                if (this._image == null && this._imagePath != null)
                {
                    this._image = new BitmapImage(new Uri(_imagePath));
                }
                return this._image;
            }

            set
            {
                this._imagePath = null;
                this.SetProperty(ref this._image, value);
            }
        }

        public void SetImage(String path)
        {
            this._image = null;
            this._imagePath = path;
            this.OnPropertyChanged("Image");
        }

        public override string ToString()
        {
            return this.Title;
        }
    }

    /// <summary>
    /// Generic item data model.
    /// </summary>
    public class SampleDataItem : SampleDataCommon
    {
        public SampleDataItem(String uniqueId, String title, String subtitle, String imagePath, String description, String content, SampleDataGroup group)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
            this._content = content;
            this._group = group;
        }

        private string _content = string.Empty;
        public string Content
        {
            get { return this._content; }
            set { this.SetProperty(ref this._content, value); }
        }

        private SampleDataGroup _group;
        public SampleDataGroup Group
        {
            get { return this._group; }
            set { this.SetProperty(ref this._group, value); }
        }
    }

    /// <summary>
    /// Generic group data model.
    /// </summary>
    public class SampleDataGroup : SampleDataCommon
    {

        private string feedUrl;

        public String FeedUrl
        {
            get
            {
                return feedUrl;
            }
            set
            {
                SetProperty(ref feedUrl, value);
            }
        }

        public SampleDataGroup(String uniqueId, String title, String subtitle, String imagePath, String description, String feedUrl)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
            this.feedUrl = feedUrl;
            Items.CollectionChanged += ItemsCollectionChanged;
        }

        private void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

            // Provides a subset of the full items collection to bind to from a GroupedItemsPage
            // for two reasons: GridView will not virtualize large items collections, and it
            // improves the user experience when browsing through groups with large numbers of
            // items.
            //
            // A maximum of 12 items are displayed because it results in filled grid columns
            // whether there are 1, 2, 3, 4, or 6 rows displayed

            const int maxItems = 6;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewStartingIndex < maxItems)
                    {
                        TopItems.Insert(e.NewStartingIndex,Items[e.NewStartingIndex]);
                        if (TopItems.Count > maxItems)
                        {
                            TopItems.RemoveAt(maxItems);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Move:
                    if (e.OldStartingIndex < maxItems && e.NewStartingIndex < maxItems)
                    {
                        TopItems.Move(e.OldStartingIndex, e.NewStartingIndex);
                    }
                    else if (e.OldStartingIndex < maxItems)
                    {
                        TopItems.RemoveAt(e.OldStartingIndex);
                        TopItems.Add(Items[maxItems - 1]);
                    }
                    else if (e.NewStartingIndex < maxItems)
                    {
                        TopItems.Insert(e.NewStartingIndex, Items[e.NewStartingIndex]);
                        TopItems.RemoveAt(maxItems);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldStartingIndex < maxItems)
                    {
                        TopItems.RemoveAt(e.OldStartingIndex);
                        if (Items.Count >= maxItems)
                        {
                            TopItems.Add(Items[maxItems - 1]);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    if (e.OldStartingIndex < maxItems)
                    {
                        TopItems[e.OldStartingIndex] = Items[e.OldStartingIndex];
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    TopItems.Clear();
                    while (TopItems.Count < Items.Count && TopItems.Count < maxItems)
                    {
                        TopItems.Add(Items[TopItems.Count]);
                    }
                    break;
            }
        }

        private ObservableCollection<SampleDataItem> _items = new ObservableCollection<SampleDataItem>();
        public ObservableCollection<SampleDataItem> Items
        {
            get { return this._items; }
        }

        private ObservableCollection<SampleDataItem> _topItem = new ObservableCollection<SampleDataItem>();
        public ObservableCollection<SampleDataItem> TopItems
        {
            get {return this._topItem; }
        }
    }

    /// <summary>
    /// Creates a collection of groups and items with hard-coded content.
    /// 
    /// SampleDataSource initializes with placeholder data rather than live production
    /// data so that sample data is provided at both design-time and run-time.
    /// </summary>
    public sealed class SampleDataSource
    {

        private static Regex laHoraImageRegex = new Regex(@"<img.*?src=""(.*?)"".*?>\s*?<p>(.*)</p>");
        private static Regex laHora2ImageRegex = new Regex(@"<img.*?src=""(.*?)"".*?>");
        private static Regex publiNewsImageRegex = new Regex(@"<img.*?src=""(.*?)"".*?>");
        private static Regex siglo21ImageRegex = new Regex(@"<img.*?src="".*?"".*?><a.*?href=""(.*?)"".*?type=""image/jpeg;.*?"".*?>");

        private static SampleDataSource _sampleDataSource = new SampleDataSource();

        private ObservableCollection<SampleDataGroup> _allGroups = new ObservableCollection<SampleDataGroup>();
        public ObservableCollection<SampleDataGroup> AllGroups
        {
            get { return this._allGroups; }
        }

        public static IEnumerable<SampleDataGroup> GetGroups(string uniqueId)
        {
            if (!uniqueId.Equals("AllGroups")) throw new ArgumentException("Only 'AllGroups' is supported as a collection of groups");
            
            return _sampleDataSource.AllGroups;
        }

        public static SampleDataGroup GetGroup(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = _sampleDataSource.AllGroups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static SampleDataItem GetItem(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = _sampleDataSource.AllGroups.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public SampleDataSource()
        {
            AllGroups.Add(new SampleDataGroup("PrensaLibre", "Prensa Libre", "Prensa Libre", null, "Prensa Libre", "http://www.prensalibre.com/rss/latest/"));
            AllGroups.Add(new SampleDataGroup("PubliNews", "Publi News", "Publi News", null, "Publi News", "http://www.publinews.gt/index.php/feed/"));
            AllGroups.Add(new SampleDataGroup("Siglo21", "Sigo 21", "Siglo 21", null, "Siglo 21", "http://www.s21.com.gt/feed/portada"));
            AllGroups.Add(new SampleDataGroup("LaHora", "La Hora", "La Hora", null, "La Hora", "http://www.lahora.com.gt/index.php?format=feed&type=rss"));

            LoadAllItems();

        }

        private void LoadAllItems()
        {
            //AllGroups.AsParallel().ForAll(LoadItemsAsyc);
            foreach (var group in AllGroups)
            {
                LoadItemsAsyc(group);
            }
        }

        private async void LoadItemsAsyc(SampleDataGroup sampleDataGroup)
        {
            var request = (HttpWebRequest) WebRequest.Create(sampleDataGroup.FeedUrl);

            var response = await  request.GetResponseAsync();

            using (var stream = response.GetResponseStream())
            {
                var reader = XmlReader.Create(stream, new XmlReaderSettings { Async = true});

                SampleDataItem newItem = null;

                while (await reader.ReadAsync())
                {

                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == "item")
                        {
                            newItem = new SampleDataItem(null, null, null, null, null, null, sampleDataGroup);
                        }
                        else if (newItem != null)
                        {
                            if (reader.Name == "title")
                            {
                                newItem.Title = await reader.ReadElementContentAsStringAsync();
                            }
                            else if (reader.Name == "link")
                            {
                                newItem.UniqueId = await reader.ReadElementContentAsStringAsync();
                            }
                            else if (reader.Name == "description")
                            {
                                newItem.Description = await reader.ReadElementContentAsStringAsync();

                                if (newItem.Description != null && newItem.Group.UniqueId == "LaHora")
                                {
                                    // la hora
                                    var match = laHoraImageRegex.Match(newItem.Description);
                                    if (match.Success)
                                    {
                                        newItem.SetImage(match.Groups[1].Value);
                                        newItem.Description = match.Groups[2].Value;
                                    }
                                    else
                                    {
                                        match = laHora2ImageRegex.Match(newItem.Description);
                                        if (match.Success)
                                        {
                                            newItem.SetImage(match.Groups[1].Value);
                                        }
                                        newItem.Description = StripHTML(newItem.Description);
                                    }
                                }
                                else if (newItem.Description != null && newItem.Group.UniqueId == "Siglo21")
                                {
                                    var match = siglo21ImageRegex.Match(newItem.Description);

                                    if (match.Success)
                                    {
                                        newItem.SetImage(match.Groups[1].Value);
                                        
                                    }

                                    newItem.Description = StripHTML(newItem.Description);
                                }

                                if (newItem.Description != null && newItem.Description.Length > 200)
                                {
                                    newItem.Description = newItem.Description.Substring(0, 200);
                                }
                            }
                            else if (reader.Name == "pubDate")
                            {
                                
                            }
                            else if (reader.Name == "author")
                            {
                                 
                            }
                            else if (reader.Name == "enclosure")
                            {
                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Name == "url")
                                    {
                                        newItem.SetImage(reader.Value);
                                    }
                                }
                            }
                            else if (reader.Name == "content:encoded")
                            {
                                var content = await reader.ReadElementContentAsStringAsync();
                                if (content != null)
                                {
                                    var match = publiNewsImageRegex.Match(content);
                                    if (match.Success)
                                    {
                                        newItem.SetImage(match.Groups[1].Value);
                                    }
                                }
                            }
                        }
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        if (reader.Name == "item")
                        {
                            sampleDataGroup.Items.Add(newItem);
                            newItem = null;  
                        }
                    }
                }
            }
            
        }

        private static string StripHTML(string htmlText)
        {
            var reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            return reg.Replace(htmlText, "");
        }
    }
}
