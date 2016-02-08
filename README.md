# code-challenge-3
Sogeti Code Challenge de Enero 2016
=====================================
Desafío #3: Biblioteca pública
-----------------------------------
*Las bibliotecas permiten tener en préstamo documentos como libros y revistas durante un período de tiempo, sin coste. Es un servicio público y como tal, no siempre está al día de la tecnología. Vamos a ayudarles con un sistema de gestión de préstamos.*
 
Escribe un programa que permita registrar libros, hacer préstamos de libros y devolver préstamos de libros. Para cada usuario se permite tener como mucho 3 libros prestados al mismo tiempo, y por un período de 30 días a partir de la fecha del préstamo. Si el usuario devuelve los libros prestados después de los 30 días, tiene que pagar una multa antes de poder tomar prestados los libros otra vez.

__Por razones de alcance, este desafío está dividido en dos.__ El siguente Code Challenge continuará con requerimientos nuevos, así que prestad especial atención en hacer un diseño flexible de vuestra solución para que pueda absorber los cambios sin tener que reescribirse desde cero.
 

Ejemplo de salida del programa
------------------------------
    ¿Quiere (R)egistrar un libro, hacer un (P)réstamo o una (D)evolución: P
    Introduzca el usuario: edin
    Introduzca el libro: HEA1
    Introduzca la fecha del préstamo: 15/01/2016
    Préstamo realizado. Fecha de devolución: 15/02/2016. 
    
    ¿Quiere (R)egistrar un libro, hacer un (P)réstamo o una (D)evolución: P
    Introduzca el usuario: roberto
    Introduzca el libro: HEA1
    El libro HEA1 (Head First Design Patterns) no está disponible ahora mismo. 
    
    ¿Quiere (R)egistrar un libro, hacer un (P)réstamo o una (D)evolución: D
    Introduzca el usuario: edin
    Introduzca el libro: HEA1
    Introduzca la fecha de la devolución: 11/02/2016
    El libro HEA1 (Head First Design Patterns) está disponible para ser prestado.
    
    
Restricciones
-------------
* Se puede usar cualquier repositorio de datos (base de datos, ficheros de texto, XML, JSON..) o ninguno (tener los datos iniciales en memoria). 
* Si usáis algún repositorio del tipo base de datos, pensad en como desplegar vuestra solución en el entorno de pruebas (mi portátil). Vuestra solución debe incluir todo lo necesario para poder probar la aplicación.
* Vigilar de que el programa resultante esté debidamente encapsulado en clases y métodos públicos y privados
* El código debe ser compatible con Visual Studio 2013 y NET Framework 4.5.2

Suposiciones
------------
* Los libros tienen como mínimo un identificador y el nombre
* Los usuarios no se tienen que registrar previamente y se identifican con un nombre de usuario
* Los usuarios pueden coger 1, 2 o 3 libros a la vez y también devolverlos todos de una vez o en días sucesivos
* La multa se pagará mediante una acción (a confirmar) en la interfaz del programa

Para nota
---------
* Guardar el estado de la biblioteca de manera persistente (base de datos, fichero o ficheros, etc)

¿Cómo subir mi código a GitHub?
===============================
En vez de enviar el código a mi correo, tenéis que hacer lo siguiente:
* Hacer un fork de este repositorio
* Crear una carpeta con vuestro nombre
* Crear vuestra solución en esa carpeta
* Hacer _commit_ en vuestro fork
* Hacer un _pull request_ para que lo incluyamos en el repositorio al final del tiempo del desafío

Tenéis una guía de como hacer un fork y pull request en GitHub [aquí](https://help.github.com/articles/fork-a-repo/)



