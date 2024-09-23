# Video cam desaf�o

## Descripci�n
Para facilitar la lectura y visualizaci�n, se separaron los proyectos de la siguiente manera:
- Cam.Con: es la aplicaci�n de consola que se menciona el el desaf�o n�mero 1
- Cam.WF: es la aplicaci�n de Windows Forms que se menciona en el desaf�o n�mero 2, que hace referencia a VideoCaptureService utilizado en la consola
- Microservice.Common, Microservice.Capture, Microservice.Queue, Microservice.Processing: son los microservicios que se mencionan en el desaf�o n�mero 3
	- Se opt� por utilizar un proyecto Common para aquellas clases/interfaces que se comparten entre los microservicios
	- Se agregaron los archivos Dockerfile correspondientes

## Prerequisitos
- Se deben cumplimentar los requisitos para el correcto funcionamiento de OpenCVCSharp: https://github.com/shimat/opencvsharp?tab=readme-ov-file#ubuntu

## Requisitos
- Docker for desktop
- Docker Compose

## Instrucciones de uso
1. Clonar el repositorio
2. Navegar al directorio del proyecto
3. Ejecutar `docker-compose up --build`

Para detener los servicios, usar `docker-compose down`

## Descripci�n de los servicios
- ServiceCapture: Captura frames de video y los publica en Kafka
- ServiceQueue: Act�a como buffer entre la captura y el procesamiento
- ServiceProcessing: Procesa los frames (resize) y publica los resultados

Los servicios utilizan el patr�n Strategy para manejar diferentes tipos de frames.