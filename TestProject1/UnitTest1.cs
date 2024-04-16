using System.Collections.Generic;
using TestProject1;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        // 1) клиент пришел в магазин - > клиент выбирает мебель(стул) в магазине -> Наличие на складе мебели -> Оплата мебели на кассе -> 
        // Выдача чека клиенту -> мебель доставлена -> Проверка качества

        //клиент пришел в магазин
        Chair storeChair = new Chair(MaterialEnum.wood, new List<Worker>(),"stul");
        Chair stockChair = new Chair(MaterialEnum.wood, new List<Worker>(),"stul");
        storeChair.price = 1000;
        Store store = new Store();
        List<Chair> chairs = new List<Chair>();
        chairs.Add(storeChair);
        chairs.Add(stockChair);
        store.chairs = new List<Chair>(chairs);
        Client client = new Client();
        client.adress = "123";
        client.goStore(store);
        bool clientInStore = store.clients.Contains(client);
        Assert.AreEqual(clientInStore, true);
        
        //клиент выбирает стул в магазине
        client.selectChair(store, "stul");
        bool contain = store.chairs.Contains(client.chair);
        Assert.AreEqual(true, contain);
        
        //Наличие на складе мебели
        client.stockChair = store.stock(storeChair, null, null);
        Assert.AreEqual(true, client.stockChair);
    
        //Оплата мебели на кассе
        Worker cashier = new Worker();
        CashRegister cashRegister = new CashRegister(cashier, client);
        cashRegister.payFurniture(client);
        Assert.AreEqual(client.payFurniture, true);
        
        //магазин выдает чек клиенту
        client.check = cashRegister.createCheck(client, storeChair.price, EnumFurniture.chair);
        Assert.AreEqual(client.check.client, client);
        Assert.AreEqual(client.check.furniture, EnumFurniture.chair);
        Assert.AreEqual(client.check.summ, 1000);
        Assert.AreEqual(client.check.checkStatus, CheckStatus.payed);
        
        //мебель доставлена
        client.delivery();

        //Проверка качества
        bool quality = client.checkQualityChair(client.chair);
        Assert.AreEqual(false, quality);
    }
    
    [Test]
    public void Test2()
    {
        // 1) клиент пришел в магазин - > клиент выбирает мебель(стул) в магазине -> Наличие на складе мебели -> Оплата мебели на кассе -> 
        // Выдача чека клиенту -> Мебель доставлена -> Проверка качества -> Возврат денег -> Доставка мебели на склад
        
        //клиент пришел в магазин
        Chair storeChair = new Chair(MaterialEnum.wood, new List<Worker>(),"stul");
        storeChair.price = 1000;
        Chair stockChair = new Chair(MaterialEnum.wood, new List<Worker>(),"stul");
        stockChair.deffect = true;
        storeChair.price = 1000;
        Store store = new Store();
        List<Chair> chairs = new List<Chair>();
        chairs.Add(storeChair);
        chairs.Add(stockChair);
        store.chairs = new List<Chair>(chairs);
        Client client = new Client();
        client.adress = "123";
        client.goStore(store);
        bool clientInStore = store.clients.Contains(client);
        Assert.AreEqual(clientInStore, true);
        
        //клиент выбирает стул в магазине
        client.selectChair(store, "stul");
        bool contain = store.chairs.Contains(client.chair);
        Assert.AreEqual(contain, true);
        
        //Наличие на складе мебели
        client.stockChair = store.stock(storeChair, null, null);
        Assert.AreEqual(true, client.stockChair);

        //Оплата мебели на кассе
        Worker cashier = new Worker();
        CashRegister cashRegister = new CashRegister(cashier, client);
        cashRegister.payFurniture(client);
        Assert.AreEqual(client.payFurniture, true);
        
        //магазин выдает чек клиенту
        client.check = cashRegister.createCheck(client, storeChair.price, EnumFurniture.chair);
        Assert.AreEqual(client.check.client, client);
        Assert.AreEqual(client.check.furniture, EnumFurniture.chair);
        Assert.AreEqual(client.check.summ, 1000);
        Assert.AreEqual(client.check.checkStatus, CheckStatus.payed);

        //Мебель доставлена
        Delivery delivery = new Delivery(client.adress, false);
        delivery.deliveryChair(stockChair);

        
        //Проверка качества
        bool quality = client.checkQualityChair(client.chair);
        Assert.AreEqual(true, quality);

        //магазин возвращает деньги клиенту
        store.returnMoneyClient(client, client.check);
        Assert.AreEqual(1000, client.budget);
        
        //Доставка мебели на склад
        delivery.returnChair(stockChair, store);
        bool returnCloset = store.chairs.Contains(delivery.chair);
        Assert.AreEqual(returnCloset, false);
    }
    
    [Test]
    public void Test3()
    {
        // 1) клиент пришел в магазин - > клиент выбирает мебель(шкаф) в магазине -> Наличие на складе мебели -> Оплата доставки ->
        // Оплата мебели на кассе -> Заказ сборщиков мебели -> 
        // Выдача чека клиенту -> Доставка мебели -> Сборка мебели -> Проверка качества
        
        //клиент пришел в магазин
        Closet closet = new Closet(MaterialEnum.plastic, new List<Worker>(), "closet");
        Closet stockCloset = new Closet(MaterialEnum.plastic, new List<Worker>(), "closet");
        closet.price = 2000;
        Store store = new Store();
        List<Closet> closets = new List<Closet>();
        closets.Add(closet);
        closets.Add(stockCloset);
        store.closets = new List<Closet>(closets);
        Client client = new Client();
        client.adress = "321";
        client.goStore(store);
        bool clientInStore = store.clients.Contains(client);
        Assert.AreEqual(true, clientInStore);
        
        //клиент выбирает шкаф в магазине
        client.selectCloset(store, "closet");
        bool contain = store.closets.Contains(client.closet);
        Assert.AreEqual(true, contain);
        
        //Наличие на складе мебели
        client.stockChair = store.stock(null, null, closet);
        Assert.AreEqual(true, client.stockChair);
        
        //Оплата доставки
        Worker worker = new Worker();
        CashRegister cashRegister = new CashRegister(worker, client);
        cashRegister.payDelivery(client);
        Assert.AreEqual(true, client.payDelivery);
        
        //Оплата мебели на кассе
        Worker cashier = new Worker();
        cashRegister.payFurniture(client);
        Assert.AreEqual(client.payFurniture, true);
        
        //Заказ сборщиков мебели
        client.CollectorFurniture = client.orderCollectorFurniture(store);

        //Выдача чека клиенту
        client.check = cashRegister.createCheck(client, closet.price, EnumFurniture.closet);
        Assert.AreEqual(client.check.client, client);
        Assert.AreEqual(client.check.furniture, EnumFurniture.closet);
        Assert.AreEqual(client.check.summ, 2000);
        Assert.AreEqual(client.check.checkStatus, CheckStatus.payed);
        
        //Доставка мебели
        Delivery delivery = new Delivery(client.adress, false);
        delivery.deliveryCloset(stockCloset);
        Assert.AreEqual(true, delivery.isCompleted);
        Assert.AreEqual(stockCloset, delivery.closet);

        //Сборка мебели 
        client.CollectorFurniture.startCollectFurniture();
        client.CollectorFurniture.endCollectFurniture();

        //Проверка качества
        bool quality = client.checkQualityCloset(stockCloset);
        Assert.AreEqual(false, quality);
    }
    
    [Test]
    public void Test4()
    {
        // 1) клиент пришел в магазин - > клиент выбирает мебель(шкаф) в магазине -> Выбор дизайна мебели -> Производство мебели на заводе ->
        //  Оплата мебели на кассе -> Выдача чека клиенту -> Проверка качества
        
        //клиент пришел в магазин
        Closet closet = new Closet(MaterialEnum.plastic, new List<Worker>(), "closet");
        closet.price = 2000;
        Store store = new Store();
        List<Closet> closets = new List<Closet>();
        closets.Add(closet);
        store.closets = new List<Closet>(closets);
        Client client = new Client();
        client.adress = "321";
        client.goStore(store);
        bool clientInStore = store.clients.Contains(client);
        Assert.AreEqual(clientInStore, true);
        
        //клиент выбирает шкаф в магазине
        client.selectCloset(store, "closet");
        bool contain = store.closets.Contains(client.closet);
        Assert.AreEqual(contain, true);
        
        //Выбор дизайна мебели
        Design design = new Design("baroko", "loft", "black");
        closet.setDesign(design);
        Assert.AreEqual("baroko", design.nameDesign);
        Assert.AreEqual("loft", design.style);
        Assert.AreEqual("black", design.color);
        
        //Производство мебели на заводе
        Worker furnitureWorker = new Worker();
        List<Worker> workers = new List<Worker>();
        workers.Add(furnitureWorker);
        FurnitureFactory furnitureFactory = new FurnitureFactory();
        Closet designCloset = furnitureFactory.createCloset(workers, MaterialEnum.metall, "closet");
        designCloset.design = design;
        furnitureFactory.workers = workers;
        Assert.AreEqual(workers, furnitureFactory.workers);
        Assert.AreEqual(workers, designCloset.workers);
        Assert.AreEqual(MaterialEnum.metall, designCloset.materialEnum);
        Assert.AreEqual("closet", designCloset.name);
        Assert.AreEqual(design, designCloset.design);

        //Оплата мебели на кассе
        Worker worker = new Worker();
        CashRegister cashRegister = new CashRegister(worker, client);
        cashRegister.payFurniture(client);
        Assert.AreEqual(client.payFurniture, true);

        //Выдача чека клиенту
        client.check = cashRegister.createCheck(client, closet.price, EnumFurniture.closet);
        Assert.AreEqual(client.check.client, client);
        Assert.AreEqual(client.check.furniture, EnumFurniture.closet);
        Assert.AreEqual(client.check.summ, 2000);
        Assert.AreEqual(client.check.checkStatus, CheckStatus.payed);
        
        //Мебель доставлена
        Delivery delivery = new Delivery(client.adress, false);
        delivery.deliveryCloset(closet);
        Assert.AreEqual(true, delivery.isCompleted);
        
        //Проверка качества
        bool quality = client.checkQualityCloset(client.closet);
        Assert.AreEqual(false, quality);
    }
    
    //Контрольный пример
    [Test]
    public void testControlExample()
    {
        // 1) клиент пришел в магазин - > клиент выбирает мебель(комод) в магазине -> Выбор дизайна мебели -> Производство мебели на заводе ->
        //  Оплата доставки -> Заказ сборщиков мебели -> Оплата мебели на кассе -> Выдача чека клиенту -> Мебель доставлена -> Сборка мебели
        // -> Проверка качества -> Возврат денег -> Доставка мебели на склад
        
        //клиент пришел в магазин
        Dresser dresser = new Dresser(MaterialEnum.plastic, new List<Worker>(), "dresser");
        Dresser stockDresser = new Dresser(MaterialEnum.plastic, new List<Worker>(), "dresser");
        dresser.price = 2000;
        Store store = new Store();
        List<Dresser> dressers = new List<Dresser>();
        dressers.Add(dresser);
        dressers.Add(stockDresser);
        store.dressers = dressers;
        Client client = new Client();
        client.adress = "321";
        client.goStore(store);
        bool clientInStore = store.clients.Contains(client);
        Assert.AreEqual(clientInStore, true);
        
        //клиент выбирает комод в магазине
        client.selectDresser(store, "dresser");
        bool contain = store.dressers.Contains(client.dresser);
        Assert.AreEqual(contain, true);
        
        //Выбор дизайна мебели
        Design design = new Design("baroko", "loft", "black");
        dresser.setDesign(design);
        Assert.AreEqual("baroko", design.nameDesign);
        Assert.AreEqual("loft", design.style);
        Assert.AreEqual("black", design.color);
        
        //Производство мебели на заводе
        Worker furnitureWorker = new Worker();
        List<Worker> workers = new List<Worker>();
        workers.Add(furnitureWorker);
        FurnitureFactory furnitureFactory = new FurnitureFactory();
        Dresser designDresser = furnitureFactory.createDresser(workers, MaterialEnum.metall, "dresser");
        designDresser.setDesign(design);
        furnitureFactory.workers = workers;
        Assert.AreEqual(workers, furnitureFactory.workers);
        Assert.AreEqual(workers, designDresser.workers);
        Assert.AreEqual(MaterialEnum.metall, designDresser.materialEnum);
        Assert.AreEqual("dresser", designDresser.name);
        Assert.AreEqual(design, designDresser.design);
        
        //Оплата доставки
        Worker worker = new Worker();
        CashRegister cashRegister = new CashRegister(worker, client);
        cashRegister.payDelivery(client);
        Assert.AreEqual(true, client.payDelivery);

        //Заказ сборщиков мебели
        client.CollectorFurniture = client.orderCollectorFurniture(store);
        Assert.IsNotNull(client.CollectorFurniture);
        
        //Оплата мебели на кассе
        cashRegister.payFurniture(client);
        Assert.AreEqual(client.payFurniture, true);
        
        //Выдача чека клиенту
        client.check = cashRegister.createCheck(client, dresser.price, EnumFurniture.closet);
        Assert.AreEqual(client.check.client, client);
        Assert.AreEqual(client.check.furniture, EnumFurniture.closet);
        Assert.AreEqual(client.check.summ, 2000);
        Assert.AreEqual(client.check.checkStatus, CheckStatus.payed);
        
        //Мебель доставлена
        Delivery delivery = new Delivery(client.adress, false);
        delivery.deliveryDresser(dresser);
        Assert.AreEqual(true, delivery.isCompleted);
        
        //Сборка мебели 
        client.CollectorFurniture.startCollectFurniture();
        Assert.AreEqual(false, client.CollectorFurniture.buildStatus);
        client.CollectorFurniture.endCollectFurniture();
        Assert.AreEqual(true, client.CollectorFurniture.buildStatus);
        
        //Проверка качества
        bool quality = client.checkQualityDresser(dresser);
        Assert.AreEqual(false, quality);

        //магазин возвращает деньги клиенту
        store.returnMoneyClient(client, client.check);
        Assert.AreEqual(2000, client.budget);
        
        //Доставка мебели на склад
        delivery.returnDresser(dresser, store);
        bool returnDresser = store.dressers.Contains(delivery.dresser);
        Assert.AreEqual(true, returnDresser);
    }

    //Инициализация объекта CashRegister
    [Test]
    public void testInitCashRegister()
    {
        Worker cashier = new Worker();
        cashier.firstName = "gleb";
        cashier.lastName = "tulegenov";
        Client client = new Client();
        client.adress = "izhevsk, izhgtu";
        client.budget = 2000;
        CashRegister cashRegister = new CashRegister(cashier, client);
        cashRegister.status = StatusCashRegister.open;
        cashRegister.check = cashRegister.createCheck(client, 1000, EnumFurniture.chair);
        
        Assert.IsNotNull(cashRegister); 
        Assert.AreEqual(StatusCashRegister.open, cashRegister.status); 
        Assert.IsNotNull(cashRegister.check); 
        Assert.AreEqual(client, cashRegister.check.client); 
        Assert.AreEqual(1000, cashRegister.check.summ); 
        Assert.AreEqual(EnumFurniture.chair, cashRegister.check.furniture);
    }
    
    
    //Тестирование метода createCheck
    [Test]
    public void testCreateCheckCashRegister()
    {
        Worker cashier = new Worker();
        Client client = new Client();
        client.budget = 2000;
        CashRegister cashRegister = new CashRegister(cashier, client);
        Check check = cashRegister.createCheck(client, client.budget, EnumFurniture.closet);
        Assert.AreEqual(client, check.client);
        Assert.AreEqual(EnumFurniture.closet,check.furniture);
        Assert.AreEqual(client.budget, check.summ);
        Assert.AreEqual(CheckStatus.payed,check.checkStatus);
    }
    
    //Тестирование метода payFurniture
    [Test]
    public void testPayFurnitureCashRegister()
    {
        Worker cashier = new Worker();
        Client client = new Client();
        CashRegister cashRegister = new CashRegister(cashier, client);
        cashRegister.payFurniture(client);
        Assert.AreEqual(true, client.payFurniture);
    }
    
    //Тестирование метода payDelivery
    [Test]
    public void testPayDeliveryCashRegister()
    {
        Worker cashier = new Worker();
        Client client = new Client();
        CashRegister cashRegister = new CashRegister(cashier, client);
        cashRegister.payDelivery(client);
        Assert.AreEqual(true, client.payDelivery);
    }
    
    //Инициализация объекта Chair
    [Test]
    public void testInitializationChair()
    {
        Worker worker = new Worker();
        worker.firstName = "gelb";
        worker.lastName = "tulegenov";
        List<Worker> workers = new List<Worker>();
        workers.Add(worker);
        Chair chair = new Chair(MaterialEnum.wood, workers, "Stul");
        Assert.AreEqual(MaterialEnum.wood, chair.materialEnum);
        Assert.AreEqual(workers, chair.workers);
        Assert.AreEqual("stul1", chair.name);
    }

    //Инициализация объекта Check
    [Test]
    public void testInitializationCheck()
    {
        Client client = new Client();
        client.adress = "123";
        client.budget = 1000;
        Check check = new Check(client, CheckStatus.notPayed, client.budget, EnumFurniture.dresser);
        Assert.AreEqual(client, check.client);
        Assert.AreEqual(CheckStatus.notPayed, check.checkStatus);
        Assert.AreEqual(client.budget, check.summ);
        Assert.AreEqual(EnumFurniture.dresser, check.furniture);
    }

    //Инициализация объекта Client
    [Test]
    public void testInitializationClient()
    {
        Store store = new Store();
        Client client = new Client();
        Worker worker = new Worker();
        worker.firstName = "gelb";
        worker.lastName = "tulegenov";
        List<Worker> workers = new List<Worker>();
        workers.Add(worker);
        Chair chair = new Chair(MaterialEnum.wood, workers, "Stul");
        client.adress = "123";
        client.budget = 1000;
        Check check = new Check(client, CheckStatus.notPayed, client.budget, EnumFurniture.dresser);
        client.check = check;
        client.quality = true;
        client.needDelivery = false;
        client.payDelivery = false;
        client.store = store;
        client.isCompleteDelivery = false;
        client.chair = chair;
        Assert.AreEqual("123", client.adress);
        Assert.AreEqual(1000, client.budget);
        Assert.AreEqual(check, client.check);
        Assert.AreEqual(true, client.quality);
        Assert.AreEqual(false, client.needDelivery);
        Assert.AreEqual(false, client.payDelivery);
        Assert.AreEqual(store, client.store);
        Assert.AreEqual(false, client.isCompleteDelivery);
        Assert.AreEqual(chair, client.chair);
    }

    //Тестирование метода checkQualityChair
    [Test]
    public void testCheckQualityChair()
    {
        Client client = new Client();
        Worker worker = new Worker();
        worker.firstName = "gelb";
        worker.lastName = "tulegenov";
        List<Worker> workers = new List<Worker>();
        workers.Add(worker);
        Chair chair = new Chair(MaterialEnum.metall, workers, "stul2");
        Assert.AreEqual(false,  client.checkQualityChair(chair));
    }
    
    //Тестирование метода checkQualityDresser
    [Test]
    public void testCheckQualityDresser()
    {
        Client client = new Client();
        Worker worker = new Worker();
        worker.firstName = "gelb";
        worker.lastName = "tulegenov";
        List<Worker> workers = new List<Worker>();
        workers.Add(worker);
        Dresser dresser = new Dresser(MaterialEnum.plastic, workers, "dresser");
        Assert.AreEqual(false,  client.checkQualityDresser(dresser));
    }
    
    //Тестирование метода checkQualityCloset
    [Test]
    public void testCheckQualityCloset()
    {
        Client client = new Client();
        Worker worker = new Worker();
        worker.firstName = "gelb";
        worker.lastName = "tulegenov";
        List<Worker> workers = new List<Worker>();
        workers.Add(worker);
        Closet closet = new Closet(MaterialEnum.metall, workers, "closet");
        Assert.AreEqual(false,  client.checkQualityCloset(closet));
    }
    
    //Тестирование метода selectChair
    [Test]
    public void testSelectChair()
    {
        Store store = new Store();
        Client client = new Client();
        Worker worker = new Worker();
        worker.firstName = "gelb";
        worker.lastName = "tulegenov";
        List<Worker> workers = new List<Worker>();
        workers.Add(worker);
        Chair chair = new Chair(MaterialEnum.metall, workers, "stul2");
        List<Chair> chairs = new List<Chair>();
        chairs.Add(chair);
        store.chairs = chairs;
        client.selectChair(store, "stul2");
        Assert.AreEqual(chair,  client.chair);
    }
    
    //Тестирование метода selectDresser
    [Test]
    public void testSelectDresser()
    {
        Store store = new Store();
        Client client = new Client();
        Worker worker = new Worker();
        worker.firstName = "gelb";
        worker.lastName = "tulegenov";
        List<Worker> workers = new List<Worker>();
        workers.Add(worker);
        Dresser dresser = new Dresser(MaterialEnum.wood, workers, "dresser");
        List<Dresser> dressers = new List<Dresser>();
        dressers.Add(dresser);
        store.dressers = dressers;
        client.selectDresser(store, "dresser");
        Assert.AreEqual(dresser,  client.dresser);
    }
    
    //Тестирование метода selectCloset
    [Test]
    public void testSelectCloset()
    {
        Store store = new Store();
        Client client = new Client();
        Worker worker = new Worker();
        worker.firstName = "gelb";
        worker.lastName = "tulegenov";
        List<Worker> workers = new List<Worker>();
        workers.Add(worker);
        Closet closet = new Closet(MaterialEnum.wood, workers, "closet");
        List<Closet> closets = new List<Closet>();
        closets.Add(closet);
        store.closets = closets;
        client.selectDresser(store, "dresser");
        Assert.AreEqual(closet,  client.closet);
    }
    
    //Тестирование метода checkChair
    [Test]
    public void testCheckChair()
    {
        Store store = new Store();
        Client client = new Client();
        Worker worker = new Worker();
        worker.firstName = "gelb";
        worker.lastName = "tulegenov";
        List<Worker> workers = new List<Worker>();
        workers.Add(worker);
        Chair chair = new Chair(MaterialEnum.metall, workers, "stul2");
        client.chair = chair;
        client.checkChair(chair);
        Assert.AreEqual(true,  client.isCompleteDelivery);
    }
    
    //Тестирование метода checkDresser
    [Test]
    public void testCheckDresser()
    {
        Store store = new Store();
        Client client = new Client();
        Worker worker = new Worker();
        worker.firstName = "gelb";
        worker.lastName = "tulegenov";
        List<Worker> workers = new List<Worker>();
        workers.Add(worker);
        Dresser dresser = new Dresser(MaterialEnum.metall, workers, "dresser");
        client.dresser = dresser;
        client.checkDresser(dresser);
        Assert.AreEqual(true,  client.isCompleteDelivery);
    }
    
    //Тестирование метода checkCloset
    [Test]
    public void testCheckCloset()
    {
        Store store = new Store();
        Client client = new Client();
        Worker worker = new Worker();
        worker.firstName = "gelb";
        worker.lastName = "tulegenov";
        List<Worker> workers = new List<Worker>();
        workers.Add(worker);
        Closet closet = new Closet(MaterialEnum.metall, workers, "closet");
        client.closet = closet;
        client.checkCloset(closet);
        Assert.AreEqual(true,  client.isCompleteDelivery);
    }
    
    //Инициализация объекта Closet
    [Test]
    public void testInitializationCloset()
    {
        Worker worker = new Worker();
        worker.firstName = "gelb";
        worker.lastName = "tulegenov";
        List<Worker> workers = new List<Worker>();
        workers.Add(worker);
        Closet closet = new Closet(MaterialEnum.metall, workers, "closet");
        Assert.AreEqual(workers, closet.workers);
        Assert.AreEqual(MaterialEnum.metall, closet.materialEnum);
        Assert.AreEqual("closet", closet.name);
    }

    //Инициализация объекта CollectorFurniture
    [Test]
    public void testInitializationCollectorFurniture()
    {
        Client client = new Client();
        Store store = new Store();
        CollectorFurniture collectorFurniture = new CollectorFurniture(client, 5, false, store);
        Assert.AreEqual(client, collectorFurniture.client);
        Assert.AreEqual(5, collectorFurniture.experience);
        Assert.AreEqual(false, collectorFurniture.buildStatus);
        Assert.AreEqual(store, collectorFurniture.store);
    }
    
    
    //Тестирование метода startCollectFurniture
    [Test]
    public void testStartCollectFurniture()
    {
        Client client = new Client();
        Store store = new Store();
        CollectorFurniture collectorFurniture = new CollectorFurniture(client, 5, false, store);
        collectorFurniture.startCollectFurniture();
        Assert.AreEqual(false, collectorFurniture.buildStatus);
    }
    
    //Тестирование метода endCollectFurniture
    [Test]
    public void testEndCollectFurnitureFurniture()
    {
        Client client = new Client();
        Store store = new Store();
        CollectorFurniture collectorFurniture = new CollectorFurniture(client, 5, false, store);
        collectorFurniture.endCollectFurniture();
        Assert.AreEqual(true, collectorFurniture.buildStatus);
    }
    
    //Инициализация объекта Delivery
    [Test]
    public void testInitializationDelivery()
    {
        Delivery delivery = new Delivery("123", false);
        Assert.AreEqual("123", delivery.deliveryAdress);
        Assert.AreEqual(false, delivery.isCompleted);
    }
    
    //Тестирование метода deliveryChair
    [Test]
    public void testDeliveryChair()
    {       
        Worker worker = new Worker();
        worker.firstName = "gelb";
        worker.lastName = "tulegenov";
        List<Worker> workers = new List<Worker>();
        workers.Add(worker);
        Chair chair = new Chair(MaterialEnum.metall, workers, "chair");
        Delivery delivery = new Delivery("123", false);
        delivery.deliveryChair(chair);
        Assert.AreEqual(chair, delivery.chair);
        Assert.AreEqual(true, delivery.isCompleted);
    }
    
    //Тестирование метода deliveryCloset
    [Test]
    public void testDeliveryCloset()
    {       
        Worker worker = new Worker();
        worker.firstName = "gelb";
        worker.lastName = "tulegenov";
        List<Worker> workers = new List<Worker>();
        workers.Add(worker);
        Closet closet = new Closet(MaterialEnum.metall, workers, "closet");
        Delivery delivery = new Delivery("123", false);
        delivery.deliveryCloset(closet);
        Assert.AreEqual(closet, delivery.closet);
        Assert.AreEqual(true, delivery.isCompleted);
    }
    
    //Тестирование метода deliveryDresser
    [Test]
    public void testDeliveryDresser()
    {       
        Worker worker = new Worker();
        worker.firstName = "gelb";
        worker.lastName = "tulegenov";
        List<Worker> workers = new List<Worker>();
        workers.Add(worker);
        Dresser dresser = new Dresser(MaterialEnum.metall, workers, "dresser");
        Delivery delivery = new Delivery("123", false);
        delivery.deliveryDresser(dresser);
        Assert.AreEqual(dresser, delivery.dresser);
        Assert.AreEqual(true, delivery.isCompleted);
    }
    
    //Тестирование метода returnCloset
    [Test]
    public void testReturnCloset()
    {       
        Worker worker = new Worker();
        worker.firstName = "gelb";
        worker.lastName = "tulegenov";
        List<Worker> workers = new List<Worker>();
        workers.Add(worker);
        Closet closet = new Closet(MaterialEnum.metall, workers, "closet");
        Delivery delivery = new Delivery("123", false);
        Store store = new Store();
        delivery.returnCloset(closet, store);
        List<Closet> closets = new List<Closet>();
        closets.Add(closet);
        Assert.AreEqual(true, closet.returnStatus);
        Assert.AreEqual(closets, store.closets);
    }
    
    //Тестирование метода returnDresser
    [Test]
    public void testReturnDresser()
    {       
        Worker worker = new Worker();
        worker.firstName = "gelb";
        worker.lastName = "tulegenov";
        List<Worker> workers = new List<Worker>();
        workers.Add(worker);
        Dresser dresser = new Dresser(MaterialEnum.metall, workers, "dresser");
        Delivery delivery = new Delivery("123", false);
        Store store = new Store();
        delivery.returnDresser(dresser, store);
        List<Dresser> dressers = new List<Dresser>();
        dressers.Add(dresser);
        Assert.AreEqual(true, dresser.returnStatus);
        Assert.AreEqual(dressers, store.dressers);
    }
    
    //Тестирование метода returnChair
    [Test]
    public void testReturnChair()
    {       
        Worker worker = new Worker();
        worker.firstName = "gelb";
        worker.lastName = "tulegenov";
        List<Worker> workers = new List<Worker>();
        workers.Add(worker);
        Chair chair = new Chair(MaterialEnum.metall, workers, "closet");
        Delivery delivery = new Delivery("123", false);
        Store store = new Store();
        delivery.returnChair(chair, store);
        List<Chair> chairs = new List<Chair>();
        chairs.Add(chair);
        Assert.AreEqual(true, chair.returnStatus);
        Assert.AreEqual(chairs, store.chairs);
    }
    
    //Инициализация объекта Design
    [Test]
    public void testInitializationDesign()
    {
        Design design = new Design("design", "style", "black");
        Assert.AreEqual("design", design.nameDesign);
        Assert.AreEqual("style", design.style);
        Assert.AreEqual("black", design.color);
    }
    
    //Инициализация объекта Dresser
    [Test]
    public void testInitializationDresser()
    {
        Worker worker = new Worker();
        worker.firstName = "gelb";
        worker.lastName = "tulegenov";
        List<Worker> workers = new List<Worker>();
        workers.Add(worker);
        Dresser dresser = new Dresser(MaterialEnum.metall, workers, "dresser");
        Assert.AreEqual(MaterialEnum.metall, dresser.materialEnum);
        Assert.AreEqual(workers, dresser.workers);
        Assert.AreEqual("dresser", dresser.name);
    }
    
    //Инициализация объекта FurnitureFactory
    [Test]
    public void testInitializationFurnitureFactory()
    {
        FurnitureFactory furnitureFactory = new FurnitureFactory();
        Worker worker = new Worker();
        worker.firstName = "gelb";
        worker.lastName = "tulegenov";
        List<Worker> workers = new List<Worker>();
        workers.Add(worker);
        furnitureFactory.workers = workers;
        Assert.AreEqual(workers, furnitureFactory.workers);
    }
    
    
    //Тестирование метода createChair
    [Test]
    public void testСreateChair()
    {
        FurnitureFactory furnitureFactory = new FurnitureFactory();
        Worker worker = new Worker();
        worker.firstName = "gelb";
        worker.lastName = "tulegenov";
        List<Worker> workers = new List<Worker>();
        workers.Add(worker);
        Chair chair = furnitureFactory.createChair(workers, MaterialEnum.plastic, "chair");
        Assert.AreEqual(workers, chair.workers);
        Assert.AreEqual(MaterialEnum.plastic, chair.materialEnum);
        Assert.AreEqual("chair", chair.name);
        Assert.AreEqual(chair, worker.chair);
    }
    
    //Тестирование метода createCloset
    [Test]
    public void testСreateCloset()
    {
        FurnitureFactory furnitureFactory = new FurnitureFactory();
        Worker worker = new Worker();
        worker.firstName = "gelb";
        worker.lastName = "tulegenov";
        List<Worker> workers = new List<Worker>();
        workers.Add(worker);
        Closet closet = furnitureFactory.createCloset(workers, MaterialEnum.plastic, "closet");
        Assert.AreEqual(workers, closet.workers);
        Assert.AreEqual(MaterialEnum.plastic, closet.materialEnum);
        Assert.AreEqual("closet", closet.name);
        Assert.AreEqual(closet, worker.closet);
    }
    
    //Тестирование метода createDresser
    [Test]
    public void testСreateDresser()
    {
        FurnitureFactory furnitureFactory = new FurnitureFactory();
        Worker worker = new Worker();
        worker.firstName = "gelb";
        worker.lastName = "tulegenov";
        List<Worker> workers = new List<Worker>();
        workers.Add(worker);
        Dresser dresser = furnitureFactory.createDresser(workers, MaterialEnum.plastic, "dresser");
        Assert.AreEqual(workers, dresser.workers);
        Assert.AreEqual(MaterialEnum.plastic, dresser.materialEnum);
        Assert.AreEqual("dresser", dresser.name);
        Assert.AreEqual(dresser, worker.dresser);
    }
    
    //Инициализация объекта Store
    [Test]
    public void testInitializationStore()
    {
        Store store = new Store();
        Assert.AreEqual(null, store.chairs);
        Assert.AreEqual(null, store.clients);
        Assert.AreEqual(null, store.closets);
        Assert.AreEqual(null, store.dressers);
        Assert.AreEqual(null, store.workers);
        Assert.AreEqual(null, store.cashRegister);
        Assert.AreEqual(null, store.collectorFurniture);
    }
    
    //Тестирование метода addFurniture
    [Test]
    public void testAddFurniture()
    {       
        Worker worker = new Worker();
        worker.firstName = "gelb";
        worker.lastName = "tulegenov";
        List<Worker> workers = new List<Worker>();
        workers.Add(worker);
        Store store = new Store();
        Chair chair = new Chair(MaterialEnum.metall, workers, "chair");
        List<Chair> chairs = new List<Chair>();
        chairs.Add(chair);
        Closet closet = new Closet(MaterialEnum.metall, workers, "closet");
        List<Closet> closets = new List<Closet>();
        closets.Add(closet);
        Dresser dresser = new Dresser(MaterialEnum.metall, workers, "dresser");
        List<Dresser> dressers = new List<Dresser>();
        dressers.Add(dresser);
        store.addFurniture(chairs, closets, dressers);
        Assert.AreEqual(chairs, store.chairs);
        Assert.AreEqual(closets, store.closets);
        Assert.AreEqual(dressers, store.dressers);
    }
    
    //Тестирование метода addWorker
    [Test]
    public void testAddWorker()
    {       
        Worker worker = new Worker();
        worker.firstName = "gelb";
        worker.lastName = "tulegenov";
        List<Worker> workers = new List<Worker>();
        workers.Add(worker);
        Store store = new Store();
        store.addWorker(workers);
        Assert.AreEqual(workers, store.workers);
    }
    
    //Тестирование метода addClient
    [Test]
    public void testAddClient()
    {
        Client client = new Client();
        List<Client> clients = new List<Client>();
        clients.Add(client);
        Store store = new Store();
        store.addClient(client);
        Assert.AreEqual(clients, store.clients);
    }
    
    //Тестирование метода findChairs
    [Test]
    public void testFindChairs()
    {
        Store store = new Store();
        List<Chair> chairs = store.findChairs("chair");
        Assert.AreEqual(new List<Chair>(), chairs);
    }
    
    //Тестирование метода findClosets
    [Test]
    public void testFindClosets()
    {
        Store store = new Store();
        List<Closet> closets = store.findClosets("closet");
        Assert.AreEqual(new List<Closet>(), closets);
    }
    
    //Тестирование метода findDressers
    [Test]
    public void testFindDressers()
    {
        Store store = new Store();
        List<Dresser> dressers = store.findDressers("dresser");
        Assert.AreEqual(new List<Dresser>(), dressers);
    }
    
    //Тестирование метода returnMoneyClient
    [Test]
    public void testReturnMoneyClient()
    {
        Store store = new Store();
        Client client = new Client();
        Check check = new Check(client, CheckStatus.payed, 1000, EnumFurniture.chair);
        store.returnMoneyClient(client, check);
        Assert.AreEqual(1000, client.budget);
    }
    
    //Инициализация объекта Worker
    [Test]
    public void testInitializationWorker()
    {
        Worker worker = new Worker();
        List<Worker> workers = new List<Worker>();
        workers.Add(worker);
        Chair chair = new Chair(MaterialEnum.metall, workers, "chair");
        Closet closet = new Closet(MaterialEnum.metall, workers, "chair");
        Dresser dresser = new Dresser(MaterialEnum.metall, workers, "chair");
        worker.chair = chair;
        worker.closet = closet;
        worker.dresser = dresser;
        worker.firstName = "gleb";
        worker.lastName = "tulegenov";
        Assert.AreEqual(chair, worker.chair);
        Assert.AreEqual(closet, worker.closet);
        Assert.AreEqual(dresser, worker.dresser);
        Assert.AreEqual("gleb", worker.firstName);
        Assert.AreEqual("tulegenov", worker.lastName);
    }
    
    //Тестирование метода goStore
    [Test]
    public void testGoStore()
    {
        Client client = new Client();
        Store store = new Store();
        client.goStore(store);
        bool clientInStore = store.clients.Contains(client);
        Assert.AreEqual(clientInStore, true);
    }
    
    //Тестирование метода orderCollectorFurniture
    [Test]
    public void testOrderCollectorFurniture()
    {
        Client client = new Client();
        Store store = new Store();
        client.CollectorFurniture = client.orderCollectorFurniture(store);
        Assert.IsNotNull(client.CollectorFurniture );
    }

}