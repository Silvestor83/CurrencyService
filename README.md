# CurrencyService
Тестовый проект был создан с рядом предположений и допущений, так как рассматривался без привязки к реальным условиям (окружение для развертывания, платформа, смежные сервисы), что может повлиять на реализацию.
В качестве платформы был выбран ASP.NET WEB API фреймворк. Развертование тестового сервиса осуществляется на IIS или IIS Express. 
В качестве сервисов по получению культуры для валюты и курсов валют использовались mock сервисы с заранее определенными данными. В реальных условиях, возможно получение данных из БД, файлов настроек и конфмгураций, а также сторонних сервисов, с навешиванием поверх справочных данных кеширования в случае необходимости.
В качестве обработчика ошибок используется стандартный обработчик ошибок ASP.NET WEB API. Не расширял его поведение, так как его начинка будет зависеть от логики стороны, которая будет использовать данный сервис.
Также не добавлялся логировщик, так как обычно используются общие решения по логированию, написаные с учетом инфраструктуры.
Также в сервисе не рассматриввались вопросы аутентификации и безопасност, так как сами по себе масштабны и зависят от многих факторов.
В сервис добавлена возможность асинхронной обработки длительно выполняющихся операций. Асинхронная логика проведена до методов, где возможно в реальных условиях использование БД и файлового ввода-вывода.
Производительность сервиса зависит от настроек iis сервера (количество одновременно выполняющихся запросов и др.). Основной времязатратный метод по округдению значений до красивых значений выполняется быстро и не требует дальнейшего распараллеливания. В реальных условиях, при замене более нагруженным, возможно выделоение отдельного потока для снижения нагрузки на поток исполнения IIS.
Примеры вызова: http://localhost:51535/api/Currency/ConvertFromUsd?price=750
http://localhost:51535/api/Currency/ConvertFromUsd?price=999&currencyTo=RUB.
