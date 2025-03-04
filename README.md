# Crazy Musicians API

**Crazy Musicians API**'ye hoş geldiniz! Bu API, müzisyenleri yönetmek için kullanılan bir sistemdir. API, müzisyen bilgilerini oluşturma, okuma, güncelleme ve silme işlemlerini yapmanıza olanak sağlar.

## API Endpoints (API Yolları)

### 1. Tüm Müzisyenleri Al
- **Yöntem**: GET
- **URL**: `/api/musicians`
- **Açıklama**: Bu endpoint, tüm müzisyenlerin listesini döndürür.
- **Yanıt**: Bir JSON dizisi olarak tüm müzisyenler döndürülür.

### 2. Müzisyen Bilgisi ID ile Al
- **Yöntem**: GET
- **URL**: `/api/musicians/{id}`
- **Açıklama**: Bu endpoint, belirli bir müzisyenin bilgilerini ID'ye göre döndürür.
- **Yanıt**: JSON formatında müzisyen bilgisi döndürülür.
- **Örnek İstek**:
    ```bash
    GET /api/musicians/1
    ```

### 3. Müzisyen Arama (İsim ile)
- **Yöntem**: GET
- **URL**: `/api/musicians/search?name={name}`
- **Açıklama**: Bu endpoint, müzisyenleri isimlerine göre arar.
- **Yanıt**: Arama sonuçları bir JSON dizisi olarak döndürülür.
- **Örnek İstek**:
    ```bash
    GET /api/musicians/search?name=John
    ```

### 4. Yeni Müzisyen Oluştur
- **Yöntem**: POST
- **URL**: `/api/musicians`
- **Açıklama**: Bu endpoint, yeni bir müzisyen ekler.
- **Gövde (Body)**: 
    ```json
    {
      "name": "John Doe",
      "profession": "Guitarist",
      "funFeature": "Plays blindfolded"
    }
    ```
- **Yanıt**: Oluşturulan müzisyen bilgisi ve 201 Created statüsü döndürülür.
- **Örnek İstek**:
    ```bash
    POST /api/musicians
    ```

### 5. Müzisyen Bilgilerini Kısmi Güncelle (JSON Patch)
- **Yöntem**: PATCH
- **URL**: `/api/musicians/{id}`
- **Açıklama**: Bu endpoint, JSON Patch kullanarak bir müzisyen bilgisini kısmi olarak günceller.
- **Gövde (Body)**: 
    ```json
    [
      { "op": "replace", "path": "/funFeature", "value": "Plays with both hands" }
    ]
    ```
- **Yanıt**: Güncellenmiş müzisyen bilgisi döndürülür.

### 6. Müzisyen Bilgilerini Tam Güncelle
- **Yöntem**: PUT
- **URL**: `/api/musicians/{id}`
- **Açıklama**: Bu endpoint, belirli bir müzisyenin tüm bilgilerini günceller.
- **Gövde (Body)**: 
    ```json
    {
      "name": "John Doe",
      "profession": "Guitarist",
      "funFeature": "Plays with both hands"
    }
    ```
- **Yanıt**: Güncellenmiş müzisyen bilgisi döndürülür.

### 7. Müzisyen Sil (ID ile)
- **Yöntem**: DELETE
- **URL**: `/api/musicians/{id}`
- **Açıklama**: Bu endpoint, belirli bir müzisyen ID'sine göre silinir.
- **Yanıt**: 204 No Content döndürülür, yani işlem başarıyla yapılır.

#### Teşşekürler
