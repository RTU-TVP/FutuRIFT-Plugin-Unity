# Futurift plugin Unity

[Rus](README.md) | [Eng](README_ENG.md)

Данный плагин позволяет управлять креслом FutuRift из приложений, написанных на Unity.

## Добавление плагина в проект

* Необходимо скачать одну из публичных версий плагина, которые можно найти [**тут**](https://github.com/RTU-TVP/Futurift-Plugin-Unity/releases)

* Скачанный *unitypackage* необходимо импортировать в ваш Unity проект

* После добавления Unity сама подтянет данный плагин, и позволит вам его использовать

## Использование плагина

Базовый элемент управления креслом - класс **FutuRiftController**

```cs
class FutuRiftController
{
    float Pitch { get; set; }
    float Roll { get; set; }
    bool IsConnected { get; }
    void Start();
    void Stop();
}
```

### Управление по сети через Futurift Controller

Для удобного использования и тестирования можно использовать [специальное ПО](https://github.com/RTU-TVP/Futurift-Controller-Emulator), выполняющее роль эмулятора кресла и контроллера.

Данный контроллер принимает команды по UDP, для его использования необходимо использовать конструктор, принимающий `UdpOptions`.

```cs
// ...
private FutuRiftController controller;

void OnEnable()
{
    controller = new FutuRiftController(new UdpPortSender(new UdpOptions 
    {
        ip = "127.0.0.1", // ip компьютера, на котором запущен контроллер
        port = 6065 // порт, на который настроен контроллер
    }));
    controller.Start();
}
// ...
```

### Управление напрямую

При необходимости можно не использовать промежуточное ПО, используя конструктор, принимающий `ComPortOptions`. Использовать конструктор, принимающий `UdpOptions`.

```cs
// ...
private FutuRiftController controller;

void OnEnable()
{
    controller = new FutuRiftController(new ComPortSender(new ComPortOptions
    {
        ComPort = 3 // Номер порта, по которому подключено кресло
    }));
    controller.Start();
}
// ...
```

#### Pitch

Свойство, меняющее или возвращающее наклон кресла вперед/назад, может принимать значения от -15 до 21.

#### Roll

Свойство, меняющее или возвращающее наклон кресла влево/вправо, может принимать значения от -18 до 18.

> Если передали значения, которые не укладываются в данный промежуток, они будут упакованы в допустимые, например при записи

```cs
sample.Roll = 120f;
sample.Pitch = -16f
```

свойству **Roll** будет присвоено значение 18, а **Pitch** -15

### Start()

Метод, который начинает посылать команды управления на кресло. Если порт удалось открыть, и данные идут, свойство **IsConnected** будет *true*

### Stop()

Метод, который останавливает управление креслом. После выполнения этого метода порт не используется программой

## Важно

В настройках проекта (Project Settings - Player) параметр Api Compatibillity Level должен иметь значение .NET Framework.

## Лицензия

Этот проект лицензирован под лицензией MIT - подробности см. в файле LICENSE.

## Контакты

Futurift Plugin Unity - это проект отдела RTU IT LAB и доработанной студентом RTU TVP Шутовым Кириллом. Если у вас есть вопросы, пожалуйста, свяжитесь со мной по электронной почте: <i@shutovks.ru>.
