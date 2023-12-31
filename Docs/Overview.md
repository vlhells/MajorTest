## Обзор приложения:  
    
Запустив приложение, Вы попадете на страницу /Orders/Index.  
На ней представлены все заявки, которые есть в базе.  
Есть пагинация. В коде контроллера страницы с курьерами настроен вывод 1 курьера на 1 стр с целью показа работы пагинации.  
    
![alt text](overviewScreens/OrdersIndexAll.PNG "/Orders/Index preview")  
    
На ней же можно найти заказ по ключевому слову (проверяются поля ФИО и телефонов):  
![alt text](overviewScreens/OrdersIndexFiltered.PNG "/Orders/Index with filter preview")  
    
Также на странице /Orders/Index можно создать новую заявку:  
![alt text](overviewScreens/OrderCreate.PNG "/Orders/Create preview")  
В случае ввода некорректных (пустых) данных будет ошибка BadRequest)  
    
У заявки можно изменить статус:  
![alt text](overviewScreens/OrderChangeState.PNG "/Orders/ChangeState preview")  
![alt text](overviewScreens/OrderChangeStateCancelled.PNG "/Orders/ChangeState (cancelled state) preview")    
![alt text](overviewScreens/OrdersIndexCancelledOrder.PNG "/Orders/Index cancelled order preview")    
    
Можно просмотреть причину отмены заявки:  
![alt text](overviewScreens/OrderCancelledReason.PNG "/Orders/CancellationReason preview") 
    
Заявку можно передать на выполнение курьеру:  
![alt text](overviewScreens/OrderSetCourier.PNG "/Orders/SetCourier preview")  
![alt text](overviewScreens/OrdersIndexWithCourier.PNG "/Orders/Index order with courier preview")   
УИД - уникальный идентификатор курьера.
	
Статус заявки можно изменить на успешное выполнение:  
![alt text](overviewScreens/OrderStateDone.PNG "/Orders/Index order with done-state preview")   
    
Также можно расширять базу курьеров и изменять их данные (здесь тоже есть фильтр по ФИО, телефонам):  
![alt text](overviewScreens/CouriersIndex.PNG "/Couriers/Index preview")  
    
## BadRequest, NotFound:  
    
BadRequest выводится либо при некорректных входных данных, либо в случае попытки удаления курьера, за которым еще закреплен заказ. NotFound - в основном, если не был найден объект с заданными параметрами.