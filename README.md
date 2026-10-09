# TP1 - Videojuegos 1

**Nombre del proyecto:** Rescate de la Gallina  
**Motor y versión:** Unity 6 LTS (6000.0.84f1)  
**Materia:** Videojuegos 1  
**Nombre:** Mailen Anaid Castro
**DNI:** 46525282

---

## Controles del Juego

- **W, A, S, D** o **Flechas:** Mover al perro por el escenario.
- **E:** Tomar / soltar a la gallina al interactuar cerca de ella.

---

## Mecánicas Implementadas

1. **Movimiento e Interacción (Consignas 1, 2 y 4):**
   - El jugador controla a un perro en tercera persona.
   - Cuenta con un sistema para recoger a la gallina (`CarryItem`), emparentándola al punto de agarre (`HoldPoint`) y mostrándola en la boca.
   - Dispone de avisos de texto en pantalla mediante TextMeshPro (`[E] Agarrar Gallina` / `[E] Soltar Gallina`) que solo aparecen cuando estamos a la distancia adecuada.

2. **Plataforma Móvil (Consigna 2):**
   - Una sección del terreno conecta dos caminos mediante una roca flotante que se mueve de un punto A a un punto B de forma continua.
   - Utiliza `Invoke` para pausar dos segundos en cada extremo antes de cambiar de dirección.
   - El perro se emparenta a la plataforma al subirse para no resbalarse ni caer al vacío mientras esta se desplaza.

3. **Obstáculo Dinámico y Spawner (Consigna 3):**
   - Un generador (`ObstacleSpawner`) colocado dentro de una cueva instancia tigres a intervalos regulares usando `InvokeRepeating`.
   - Cada tigre corre en línea recta cruzando el sendero en dirección a la segunda cueva.
   - Para no saturar la memoria ni la jerarquía, cada clon se elimina automáticamente tras unos segundos mediante `Destroy(gameObject, lifeTime)`.

4. **Potenciador Temporal / Power-Up (Consigna 5):**
   - **Habilidad modificada:** Velocidad de movimiento del personaje.
   - En el camino se pueden recoger hongos mágicos que otorgan un incremento notable de velocidad durante 4 segundos.
   - El efecto y el tiempo de recarga (cooldown de 5 segundos) se gestionan mediante corrutinas (`IEnumerator`).
   - Durante la recarga no se desactiva el objeto entero, sino que se oculta su malla visual y se apaga su collider para permitir que la corrutina complete el ciclo de reaparición.

5. **Zona de Meta / Victoria (Consigna 6):**
   - Plataforma final que cuenta con un `Trigger` para validar la llegada con la gallina.
   - Si entramos con la gallina en la boca, nos avisa con un mensaje para depositarla con `E`.
   - Al soltar la gallina dentro de la meta, la superficie cambia visualmente de color a verde y se muestra el mensaje de felicitaciones por haber completado el nivel.

---

## Captura del Escenario

![Captura del Juego](CapturaJuego.png)