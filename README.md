Простая библиотека реализующая логирование в файл и по http протоколу на сервер. 

Пример

IFileLogger logger=new FileLogger();
logger.Write<Class>("message");

IHttpLogger loggerhttp=new HttpLogger();
loggerhttp.Url="http://127.0.0.1";
loggerhttp.Write<Class>("message");

