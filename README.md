# FutuRIFT plugin Unity

[Rus](README.md) | [Eng](README.en.md)

Плагин позволяет взаимодействовать с программой управляющей креслом FutuRIFT в Unity.

## Добавление плагина в проект

Вы можете установить плагин любым из следующих способов

* Скачать unitypackage:
  * Скачать одну из публичных версий плагина, которые можно найти [**здесь**](https://github.com/RTU-TVP/FutuRIFT-Plugin-Unity/releases).
  * Импортировать скачанный *unitypackage* в ваш проект Unity.
  * После добавления Unity автоматически подтянет плагин, позволяя вам его использовать.

* С помощью Package Manager (для Unity 2019.3 и выше):
  * Открыть Package Manager в Unity.
  * Нажать на кнопку **+** в верхнем левом углу окна Package Manager.
  * Выбрать **Add package from git URL**.
  * Ввести ссылку на репозиторий плагина: `https://github.com/RTU-TVP/FutuRIFT-Plugin-Unity.git?path=src/FutuRIFT-Plugin/Assets/Plugins/FutuRIFT`

* Скачать исходный код:
  * Склонировать репозиторий плагина.
  * Скопировать папку **Plugins/FutuRIFT** в папку **Assets** вашего проекта.

## Использование плагина

Базовый элемент управления креслом - класс **FutuRIFTController**

```cs
// Класс <c>FutuRIFTController</c> предназначен для управления устройством FutuRIFT через отправку UDP-сообщений.
class FutuRIFTController
{
    // Свойство для чтения и записи значения угла тангажа.
    // Наклон вперед или назад.
    // Значение ограничено в диапазоне от -15 до 21.
    float Pitch { get; set; }

    // Свойство для чтения и записи значения угла крена.
    // Наклон влево или вправо.
    // Значение ограничено в диапазоне от -18 до 18.
    float Roll { get; set; }

    // Конструктор класса <c>FutuRIFTController</c>.
    // "ip">IP-адрес по которому будет отправляться UDP-сообщение.
    // "port">Порт по которому будет отправляться UDP-сообщение.
    public FutuRIFTController(string ip = "127.0.0.1", int port = 6065)

    // Метод для запуска отправки UDP-сообщений.
    void Start();
    
    // Метод для остановки отправки UDP-сообщений.
    void Stop();
}
```

### Управление по сети через FutuRIFT Controller

Для удобного использования и тестирования можно использовать [специальное ПО](https://github.com/RTU-TVP/FutuRIFT-Controller-Emulator), выполняющее роль эмулятора кресла и контроллера.

Данный контроллер принимает команды по UDP, для его использования необходимо использовать конструктор, принимающий ip адрес и порт.

```cs
// ...
private FutuRIFTController controller;

void OnEnable()
{
    controller = new FutuRIFTController("127.0.0.1", 6065);
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

Метод, который начинает посылать команды управления на кресло.

### Stop()

Метод, который останавливает управление креслом.

## Лицензия

Этот проект лицензирован под лицензией MIT - подробности см. в файле LICENSE.

## Контакты

FutuRIFT Plugin Unity - это проект отдела RTU IT LAB и доработанной студентом RTU TVP Шутовым Кириллом. Если у вас есть вопросы, пожалуйста, свяжитесь со мной по электронной почте: <i@shutovks.ru>.
