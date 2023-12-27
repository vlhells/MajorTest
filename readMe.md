Скришноты и путеводитель по приложению находятся в папке /Docs.  
    
## Инструкция по развёртке, самый простой вариант:
	1) Скачать на компьютер Visual Studio (2019/2022 - за более ранние версии не ручаюсь).
	2) При установке добавить пакеты .NET (C#) классические приложения, ASP.NET.
	3) Скачать PostgreSQL. Запомнить пароль мастер-пользователя и открытый порт. Либо позже придется копаться в файлах настроек)
	4) По пути ~/MajorTest/MajorTest/appsettings.json заменить данные (пароль и порт, в общем случае) на свои из postgres.
	5) Запустить приложение в VS.
	
	При первых запусках может быть долгая сборка.

## Возможные ошибки:
	1) Проблема с пакетами nuget - нужно их просто перегрузить. В VS - в одной из верхних вкладок (на русском - Средства).  
	Примерно посередине верхнего меню.
	Либо же в меню с файлами проекта (ПКМ по проекту, Nuget).