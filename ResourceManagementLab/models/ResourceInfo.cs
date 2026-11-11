namespace ResourceManagementLab.Models;

/*
Задание 9. Когда нужен IDisposable?

| Класс                                              | Нужен IDisposable? | Почему |
|----------------------------------------------------|--------------------|--------|
| Player (string Name, int Score)                    | Нет                | только управляемая память |
| FileStorage (владеет FileStream)                   | Да                 | FileStream владеет системным ресурсом |
| Report (List<string>)                              | Обычно нет         | List — управляемая память, GC справится |
| SocketManager (владеет Socket)                     | Да                 | Socket — внешний ресурс |
| Класс, принимающий чужой Stream и читающий из него | Не обязательно     | не владеет потоком, закрывать не должен |
*/
public static class ResourceInfo { }