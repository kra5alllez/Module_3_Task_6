# Module_3_Task_6
Создать Logger с логикой асинхронной записи в файл.

Logger содержит событие, которое уведомляет, о том когда необходимо делать резервную копию файла логов.

На события Logger’a подписан класс Starter

Резервную копию (Backup) - делать после каждой N записи в лога.
Резервные копии это 
- файл с именем в виде времени создания файла 
- каждый новый файл содержит на N записей больше
- все файлы копий лежат в папке Backup
- N - это конфигурируемое число из файла
Запустить 2 асинхронных метода, каждый из которых сделает по 50 записей в логи
