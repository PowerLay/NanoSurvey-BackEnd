# NanoSurvey-BackEnd
BackEnd часть сайта с опросами. 
## Технологии
* MS SQL
* Entity Framework Core
* ASP.Net core API  
##  http запросы
1. Get
   1. Не принимает аргументов.
   1. Возвращает все вопросы с вариантами ответов из базы данных.
1. Get/id
   1. Возврщает вопрс с варинтами ответов по id.
1. Post
   1. Принимает 3 id массивом 
      1. InterviewId
      1. QuestionId
      1. AnswerId
   1. Возвращет созданный Result.
