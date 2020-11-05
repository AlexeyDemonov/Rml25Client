# Rml25Client

Клиент для получения информации с сервера устройств RML25

Changelog:

1.0.6
- Ошибки в процессе работы (после получения списка устройств) больше не приводят к закрытию приложения, только показывается окно с ошибкой

1.0.5
- Поправил зависание проги раз в день (надеюсь)

1.0.4
- Добавлен вывод доп. инфы по полученным данным над таблицей и графиком (название датчика, заряд батареи и уровень сигнала)
  * Для заряда и сигнала берутся крайние (самые новые) значения из соотв. таблиц независимо от того, за какой период строится отчёт по уровню воды
- Улучшен внешний вид таблицы отчёта (по крайней мере я надеюсь так стало симпатичнее)
- Добавлена возможность уменьшить и/или скрыть контрольную панель справа (зажать и тащить вправо серую вертикальную линию)
- Улучшена кодовая база (снаружи не видно, но внутри стало чуточку красивее)

1.0.2
- Сохранение всех данных для входа
    * Первый раз придётся ввести ручками (= На второй только нажать кнопку
    * Пароль сохраняется в зашифрованном и закодированном виде, достать и расшифровать можно, но "тупо блокнотиком" уже не получится, а хакнуть можно всё, так что считаю такую меру защиты достаточной
- Автообновление графика и таблицы

1.0.0
- Первый выпуск
