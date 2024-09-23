# Video cam desafío

## Descripción
Para facilitar la lectura y visualización, se separaron los proyectos de la siguiente manera:
- Cam.Con: es la aplicación de consola que se menciona el el desafío número 1
- Cam.WF: es la aplicación de Windows Forms que se menciona en el desafío número 2, que hace referencia a VideoCaptureService utilizado en la consola
- Microservice.Common, Microservice.Capture, Microservice.Queue, Microservice.Processing: son los microservicios que se mencionan en el desafío número 3
	- Se optó por utilizar un proyecto Common para aquellas clases/interfaces que se comparten entre los microservicios
	- Se agregaron los archivos Dockerfile correspondientes

## Prerequisitos
- Se deben cumplimentar los requisitos para el correcto funcionamiento de OpenCVCSharp en los microservicios en docker: https://github.com/shimat/opencvsharp?tab=readme-ov-file#ubuntu

## Requisitos
- Docker for desktop
- Docker Compose

## Instrucciones de uso para la consola (desafío 1)
1. Clonar el repositorio
2. Establecer al proyecto Cam.Con como proyecto de inicio
3. Ejecutar la aplicación

## Instrucciones de uso para el Windows Form (desafío 2)
1. Clonar el repositorio
2. Establecer al proyecto Cam.WF como proyecto de inicio
3. Ejecutar la aplicación
4. Con el ComboBox se puede elegir si se utilizará la webcam o un archivo
	* Clic en "Select file" para seleccionar un archivo
	* Alternativamente, dejar seleccionado "Webcam (default)" para utilizar la cámara detectada
5. Clic en "Start" para que inicie la operación. Se verá el resultado en el Form
6. Una vez terminada la operación, clic en "Stop" para liberar los servicios

## Instrucciones de uso para los microservicios (desafío 3/4)
1. Clonar el repositorio
2. Navegar al directorio del proyecto
3. Ejecutar `docker-compose up --build`

Para detener los servicios, usar `docker-compose down`

## Descripción de los servicios
- ServiceCapture: Captura frames de video y los publica en Kafka
- ServiceQueue: Actúa como buffer entre la captura y el procesamiento
- ServiceProcessing: Procesa los frames (resize) y publica los resultados

Los servicios utilizan el patrón Strategy para manejar diferentes tipos de frames.