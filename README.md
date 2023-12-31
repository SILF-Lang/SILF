# SILF Script runtime

SILF Script es un pequeño lenguaje de scripting para .NET


# Tipos de datos

#### Valor.

Los tipos de datos por valor se refieren a aquellos en los que la variable guarda directamente el valor asignado, en lugar de una referencia a la ubicación de memoria donde se almacena el valor. En estos casos, cuando se asigna una variable a otra, se copia el valor, y las dos variables son independientes.

* ```string``` Cadenas de texto.
* ```number``` Números.
* ```char``` Caracteres.
* ```bool``` Valores booleanos ```true``` o ```false```.
* ```int``` Números enteros.




# Variables

#### Inferencia de tipo.
Declaración de variables con inferencia de tipos.
```java
let name = "Alex"
```

Las variables con inferencia de tipo, toman el tipo del primer valor que llega.

En el caso ```name``` es del tipo ```string```.


---
#### Tipo explícito
Declaración de variables con tipo explícito de tipos.
```java
string name = "Juan"
```

# Listas


Una lista es una estructura de datos en la que los elementos se almacenan de manera secuencial. Cada elemento tiene una posición única dentro de la lista, identificada por su índice. Las listas son una forma eficaz de gestionar colecciones de elementos del mismo tipo.

### Declaración

Al igual que en muchos lenguajes con JavaScript o C# desde la versión 12, en SILF se declaran las listas usando ```[``` ```]```

```java
let ages = [1, 5, 22, 82, 98]
```

### Índices

El índice inicia en número 0 al igual que otros lenguajes como Java, C#, C++ o JS.


# Aritmética

SILF sigue el orden ```PEMDAS```.


# Interpolación de cadenas

La interpolación de cadenas permite concatenar varias cadenas / objetos sin la necesidad del operador ```+```.

```java
string name = "world"
print("Hello ${name}")
print("Hello £{name}")
print("Hello ¥{name}")
```

Puede usar ```$```, ```£``` o ```¥```.

# Palabras clave

En SILF existen unas palabras clave que salen del común de otros lenguajes de programación.


### Previous

La palabra clave ```previous``` permite obtener el valor anterior de una variable.

```java
string name = "Alex"
name = "Juan"
print(name) // Juan
print(previous name) // Alex
```

Esta palabra no sobreescribe el valor actual.


### Clear

La palabra clave ```clear``` permite borrar el historial de una variable, esto para ahorrar y liberar memoria.

```java
string name = "Alex"
name = "Juan"
clear name
print(name) // Juan
print(previous name) // 'Juan', ya que no existe un historial.
```

Esta palabra no sobreescribe el valor actual.


# Otras características

Otras características del lenguaje son:

- Funciones.
- Scope en variables.
- Ciclos
- Condicionales