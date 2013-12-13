GtPress
======

GtPress es una aplicación para [Windows 8][WIN8] que permite leer las ultimas noticias de los periódicos de Guatemala en una sola interfaz.

Características:
--------------

- Reconoce las ultimas noticias de [Prensa Libre][PL], [Publinews][PN], [Siglo 21][S21] y [La Hora][LH] y las despliega agrupadas.
- Permite compartir una noticia por medio de las redes sociales utilizando las interfaces estándar de [Windows 8][WIN8].
- Identifica los distintos formatos utilizados por los RSS de cada sitio y los interpreta para poder desplegar correctamente las imágenes y las descripciones de las noticias.

To DO
-----

Algunas de las cosas que quedaron pendientes de implementar son:

- Un lector mas amigable para las noticias (que oculte los marcos del sitio original, publicidad, etc.)
- Guardar en cache las noticias (actualmente depende de la conexion a internet, y no guarda localmente ninguna noticia).
- Actualizar frecuentemente las noticias (actualmente hay que cargar nuevamente la pagina principal para obtener las ultimas noticias).
- Validar que exista una conexion a internet o desplegar la noticias en modo offline si no se cuenta con conexion a internet.
- Desplegar los comentarios de las noticias que los usuarios han colocado en el sitio oficial.

Capturas de pantalla:
-------------------

[![Pantalla principal][IMG01]][IMG01]
> Pantalla principal.

[![Listado de noticias][IMG02]][IMG02]
> Listado de noticias de un periódico especifico *esta vista se muestra cuando se da clic en el titulo del periodico*.

[![Detalle de la noticia][IMG03]][IMG03]

> Lector WebView que permite leer la noticia en su formato original.

[![Compartir][IMG04]][IMG04]
> Ociones para compartir una noticia en especifica *esta lista se despliega desde la opción compartir estándar de [windows 8][WIN8] desde la barra del lado derecho de la pantalla*

[![Compartir en Facebook][IMG05]][IMG05]
> Ejemplo de una noticia compartida en [Facebook][FB].

[WIN8]:  http://windows.microsoft.com/en-us/windows-8/meet "Windows 8"
[PL]:    http://www.prensalibre.com/ "Prensa Libre"
[PN]:    http://www.publinews.gt/ "Publinews"
[S21]:   http://www.s21.com.gt/ "Siglo XXI"
[LH]:    http://www.lahora.com.gt/ "La Hora"
[FB]:    https://www.facebook.com/ "Facebook"
[IMG01]: https://qdkimw.dm2302.livefilestore.com/y2pNxoYSdaWzObdLxjksnc_EO0t4fYRK2cHSEE8dIMA2E2Z7i3314STbVt862oHGEWlAMVKa9pyQIbf8x27HzZABEyRYhdXzcBwQaqqspHiAkk/01-principal.png?psid=1 "Pantalla Principal"
[IMG02]: https://7tkimw.dm1.livefilestore.com/y2p7dZPV8CPf2TPBmzexN-vRs9pbI64PSP5TnjAwdKnuSIePohjtn-7kHqcjeS7ZTkQ-iEc0fu6ny-Ma6nucM0tf_vFJt_qdGnY5Hnsm--8Qho/02-listado.png?psid=1 "Listado de noticias"
[IMG03]: https://7tkimw.dm2304.livefilestore.com/y2pdKNq35SiwIKx0izURGSjk3Y2sxNgY5tSBgecbCbkM3spXil8WcMLO5JkXB9Iys_HKxnSezvmFbmL9yvJb02T6VFZlLyekNvDIxn6u523N3Q/03-detalle.png?psid=1 "Detalle de la noticia"
[IMG04]: https://7tkimw.dm2302.livefilestore.com/y2pOHSB6O8qugJiaDVSfonA8Sv4abAuWpJ-4vkiqLWn8TttGDrzqFKZtcWcGSAWL7hK5St3QcX3a6J-AnCo84cqoBRPFLwLqdVKsVKgk7DCAfU/04-compartir.png?psid=1 "Compartir"
[IMG05]: https://7tkimw.dm1.livefilestore.com/y2prN5-IzvEO8K1oUjgCDzzcJoG4QtLaRqK6lBMQCgQY485Y1gQDTBL8Z2zSpDEXaCtnFQDkB_FIX09aaeYnIOBYWAhQmQ7yNiMnmBMOkjqmic/05-compartir.png?psid=1 "Compartir en Facebook"
