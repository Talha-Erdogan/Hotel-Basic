Asp.Net Core MVC - Otel Uygulaması (Basit)

* Asp.net Core Mvc (3.1) ile oluşturulmuştur.

Katmanlı mimari yapısı ile proje geliştirilmiştir. Mimari 3 katmandan oluşmaktadır:
- Hotel.Business
- Hotel.Data
- Hotel.Web

Proje katmanlarından kısaca bahsedecek olursak;

* Hotel.Business : Sql veri tabanı bağlantılarının sağlandığı, isteklerin atıldığı katmandır. Hotel.Web katmanının tüm isteklerindeki "ekleme,güncelleme,silme ve listeleme" işlevlerinin alt yapısının oluşturulduğu katmandır.
* Hotel.Data : Sql veri tabanındaki tabloların bulunduğu katmandır. Tablo isimleri ve property'lerini içermektedir. Ayrıyeten Entity Framework ile Sql bağlantılarının sağlandığı "DbConnection" bilgilerinin bulunduğu katmandır.
* Hotel.Web : Asp.Net Core Mvc Web API (3.1) ile kodlanmıştır. Web katmanından yönetim işlevleri ayarlanıp, veri ekleme, silme , düzenleme ve listeleme işlevlerinin yapılması sağlanır. Ayrıyeten kullanıcılara erişim yetkilerinin verilmesi veya silinmesi işlevlerinin ayarlandığı katmandır. Buradaki işlevlerden biri de Login olan kişilerin yetkisi dahilinde bulunduğu işlevleri yapabilmesidir. Örneğin, oda ekleme, silme ve listeleme profiline sahip kullanıcı sadece kendi yetkisi dahilindeki işlevleri yapabilir. Buradaki işlevler de yetki grupları oluşturulup, yetki gruplarına kişiler eklenmektedir. Bu sayede Login olan kişinin yetkisi bulunduğu kısımlara erişimi sağlanmaktadır.

Kullanılan Teknolojiler:

- Asp.Net Core Mvc(3.1)
- Sql
- Entity Framework
- Dependency Injection
- Authorization Filter
- Paginition Algoritması (CurrnetPage,PageSize,From,To ... )
- Core UI Teması
- Bootstrap
