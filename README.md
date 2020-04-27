# Hospital_managment_system
Є 3 види юзерів: Admin, Doctor, Patient.
При першому запуску програми з пустою базою даних, створюються три юзери з цими ролями і записуються в базу даних.
Дані для входу:
* Admi: admin@test.com 
          Password123
* Doctor: doctor@test.com
            Password123
* Patient: patient123@test.com
              Password123
              
Кожен юзер має свій профіль. Реєструвати лікарів може тільки адмін, реєструвати пацієнтів може тільки лікар, додавати інформацію про пацієнтів може тільки лікар цього пацієнта.
Додавати пости на головну сторінку може тільки адмін. Редагувати інформацію про лікаря може лікар і адмін.


На сторінці адміна є списуок усіх лікарів(Адмін може перейти на сторінку лікаря)

На сторінці лікаря є список всіх пацієнтів(Лікар може перейти на сторінку пацієнта)

На сторінці пацієнта є уся історія хвороби(Пацієнт мже перейти на сторінку Свого лікаря)

# Hospital_managment_system API
Список всіх лікарів в форматі json
```
GET: {localhost}/api/Doctors
```
Лікар з певним Id в форматі json
```
GET: {localhost}/api/Doctors/{id}
```
Лікар з певним UserName в форматі json(UserName == Email)
```
GET: {localhost}/api/Doctors/ByUserName/{username}
```
Додати пацієнта певному лікарю
```
POST: {localhost}/api/Doctors/AddPatient/{email}/{password}

Body: {
  string FirstName
  string LastName
  int Age
  int Chamber
  string Diagnosis
  string Email
  string Password
}
```
Усі пацієнти в форматі json
```
GET: {localhost}/api/Patients
```
Усі пацієнти певного лікаря 
```
GET: {localhost}/api/Patients/ByDoctorId/{doctorId}
```
Пацієнт з певним Id в форматі json
```
GET: {localhost}/api/Patients/ById/{id}
```
Пацієнт з певним UserName
```
GET: {localhost}/api/Patients/ByUserName/{username}
```

# Own instance
0. Запустіть Hospital_managment_system/Hospital_managment_systemю.sln
1. В файлі appsettings.json вставте строку підключення до MS SQL Server.
2. З проекту DataBase в консолі диспетчера пакетів виконайте Update-Database.
3. Запустіть програму.
