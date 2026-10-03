# 🚀 Asteroids Clone - Unity 2D Technical Documentation

Bu proje, Atari'nin klasik 1979 yapımı **Asteroids** oyununun modern Unity 2D motoru ve **SOLID prensipleri** ile yeniden geliştirilmiş sürümüdür. Modüler mimari, arayüz tabanlı (Interface-driven) tasarım ve nesne yönelimli teknikler kullanılarak temiz ve sürdürülebilir bir kod tabanı hedeflenmiştir.

## 📌 İçindekiler

1. [Sistem Mimarisi ve Tasarım Kalıpları](#-sistem-mimarisi-ve-tasarım-kalıpları)

2. [Detaylı Klasör ve Dosya Hiyerarşisi](#-detaylı-klasör-ve-dosya-hiyerarşisi)

3. [Çekirdek Sistemler ve Kod Analizi](#-çekirdek-sistemler-ve-kod-analizi)

   * [Arayüzler (Interfaces)](#1-arayüzler-interfaces)

   * [Yöneticiler (Managers)](#2-yöneticiler-managers)

   * [Kontrolcüler (Controllers)](#3-kontrolcüler-controllers)

   * [Oyuncu ve Aktörler](#4-oyuncu-ve-aktörler)

4. [Fizik ve Matematiksel Mantık](#-fizik-ve-matematiksel-mantık)

5. [Skor ve Oyuncu Denge Tablosu](#-skor-ve-oyuncu-denge-tablosu)

6. [Kurulum ve Derleme (Build) Adımları](#-kurulum-ve-derleme-build-adımları)

## 🏗️ Sistem Mimarisi ve Tasarım Kalıpları

Proje, bağımlılıkları en aza indirmek (Decoupling) ve test edilebilirliği artırmak adına üç katmanlı bir yapıda tasarlanmıştır:

```
┌──────────────────────────────────────────────────────────────────────────────────┐
│                                   PRESENTATION                                   │
│  ├── UiManager (HUD, Can, Skor)           ├── SoundManager (SFX, Ritmik Sesler)  │
│  ├── MenuUiManager (Ana Menü, Ayarlar)     └── PlayerVisual (Animator/Effects)    │
└────────────────────────────────────────┬─────────────────────────────────────────┘
                                         │
┌────────────────────────────────────────┴─────────────────────────────────────────┐
│                                WORLD & SPAWNING                                  │
│  ├── SpawnManager (Asteroit Oluşturucu)  ├── AsteroidController (Parçalanma)    │
│  ├── EShipSpawnManager (Düşman Gemisi)   └── Destroyer (Alan Dışı Temizlik)       │
└────────────────────────────────────────┬─────────────────────────────────────────┘
                                         │
┌────────────────────────────────────────┴─────────────────────────────────────────┐
│                                   GAMEPLAY                                       │
│  ├── PlayerController (Ana Mantık)       ├── MoveController (İvme/Hız)          │
│  ├── SwapController (Ekran Sarmalama)    ├── PlayerInput (Girdi Yönetimi)        │
│  └── Bullet (Mermi Fiziği)               └── Interfaces (IDamagable, IMoveable)  │
└──────────────────────────────────────────────────────────────────────────────────┘

```

### Kullanılan Tasarım Kalıpları:

* **Singleton Pattern**: `ScoreManager`, `SoundManager` ve `UiManager` gibi sahne boyunca tekil olması gereken ve küresel erişim gerektiren yapılar için uygulandı.

* **Interface Segregation**: Nesnelerin yetenekleri küçültülmüş arayüzlere bölündü (`IDamagable`, `IMoveable`, `IRotateable`, `IShootable`).

* **Component-Based Architecture**: Fizik, hareket, görsel efekt ve girdi işleme süreçleri bağımsız `MonoBehaviour` bileşenlerine ayrıldı.

## 📂 Detaylı Klasör ve Dosya Hiyerarşisi

```
Assets/
└── _GameAssets/
    ├── Animation/
    │   └── Player/
    │       ├── Idle.anim                      # Geminin durgun durum animasyonu
    │       ├── Thruster.anim                  # Motor itiş alevi animasyonu
    │       └── PlayerVisual.controller        # Animator Controller geçişleri
    ├── Fonts/
    │   └── SF Atarian System/                 # Arcade temalı tipografi varlıkları
    ├── Prefabs/
    │   ├── Asteroids/
    │   │   ├── BaseAsteroid.prefab            # Temel asteroit şablonu
    │   │   ├── Big/ (V1, V2, V3)              # Büyük boy asteroit varyasyonları
    │   │   ├── Mid/ (V1, V2, V3)              # Orta boy asteroit varyasyonları
    │   │   └── Small/ (V1, V2, V3)            # Küçük boy asteroit varyasyonları
    │   ├── Effects/
    │   ├── Enemy/
    │   │   ├── EnemyShip.prefab               # Düşman uzay gemisi (Saucer)
    │   │   └── BulletVE.prefab                # Düşman mermisi
    │   └── Player/
    │       ├── Player.prefab                  # Oyuncu uzay gemisi
    │       └── BulletVP.prefab                # Oyuncu mermisi
    ├── Scenes/
    │   ├── MainMenu.unity                     # Giriş ve ayarlar sahnesi
    │   └── Game.unity                         # Ana oynanış sahnesi
    ├── Scripts/
    │   ├── Interfaces/
    │   │   ├── IDamagable.cs                  # Hasar alma/yok olma sözleşmesi
    │   │   ├── IMoveable.cs                   # İlerleme hareketi sözleşmesi
    │   │   ├── IRotateable.cs                 # Açısal dönüş sözleşmesi
    │   │   └── IShootable.cs                  # Mermi fırlatma sözleşmesi
    │   ├── Managers/
    │   │   ├── Controllers/
    │   │   │   ├── AnimationController.cs     # Animasyon durum yönetimi
    │   │   │   ├── MoveController.cs          # Fizik tabanlı ivmelenme
    │   │   │   └── SwapController.cs          # Ekran kenarı geçişi (Wrap)
    │   │   └── GameManagers/
    │   │       ├── EShipSpawnManager.cs       # Düşman gemisi zamanlayıcısı
    │   │       ├── MenuUiManager.cs           # Menü UI etkileşimleri
    │   │       ├── ScoreManager.cs            # Puan ve HighScore takibi
    │   │       ├── SoundManager.cs            # Ses FX ve ritmik arka plan
    │   │       ├── SpawnManager.cs            # Asteroit dalga yönetimi
    │   │       └── UiManager.cs               # HUD, can ve game over paneli
    │   ├── Others/
    │   │   └── Destroyer.cs                   # Zaman/Mesafe bazlı temizleyici
    │   ├── Player/
    │   │   ├── Bullet.cs                      # Mermi hareket ve çakışma mantığı
    │   │   ├── PlayerController.cs            # Oyuncu ana kontrol sınıfı
    │   │   └── PlayerInput.cs                 # Klavye/Gamepad girdi okuyucu
    │   └── Threats/
    │       ├── AsteroidController.cs          # Asteroit hareketi ve bölünme
    │       └── EnemyShipController.cs         # Düşman AI ve hedefleme
    ├── Sounds/                                # Ses efektleri (.wav, .mp3)
    └── Textures/                              # Sprite'lar ve PhysicsMaterial2D

```

## ⚙️ Çekirdek Sistemler ve Kod Analizi

### 1. Arayüzler (Interfaces)

Projedeki tüm aktörler bağımsızlığını korumak için sözleşmeler (Interfaces) üzerinden haberleşir.

* **`IDamagable`**: Hasar alabilen veya nesneye çarptığında yok olan tüm varlıklar tarafından türetilir.

  ```
  public interface IDamagable
  {
      void TakeDamage(int amount);
      void Die();
  }
  
  ```

* **`IMoveable` & `IRotateable`**: Nesnelerin fiziksel hareket niteliklerini standartlaştırır.

* **`IShootable`**: Mermi atma mekaniğine sahip birimlerin (`Player`, `EnemyShip`) ortak metodudur.

### 2. Yöneticiler (Managers)

#### **`SpawnManager.cs`**

* Oyuncunun bulunduğu ekranın dış sınırlarında rastgele koordinatlar üretir.

* Zorluk derecesine göre büyük asteroitleri sahneye sürer.

* Asteroit yönlerini ekranın merkezine doğru hafif bir sapma açısıyla ayarlar.

#### **`EShipSpawnManager.cs`**

* Rastgele zaman aralıklarında (örneğin 20-40 saniye arası) düşman uzay gemisini sahnede oluşturur.

* Düşman gemisi ekranı yatay düzlemde katederken oyuncuyu hedefler.

#### **`SoundManager.cs`**

* **Ritmik Arka Plan (Heartbeat Effect)**: Klasik Asteroids hissiyatını vermek için iki farklı bas tonunu (`beat1`, `beat2`) asteroit sayısı azaldıkça hızlanan bir tempoyla çalar.

* Ses efektleri için kanal çakışmalarını önleyen `AudioSource` havuzu kullanır.

#### **`ScoreManager.cs`**

* Oyundaki mevcut puanı ve yerel depolamadaki (`PlayerPrefs`) en yüksek skoru tutar.

* Kırılan asteroitin boyutuna göre dinamik puan eklemesi yapar.

### 3. Kontrolcüler (Controllers)

#### **`SwapController.cs` (Screen Wrap - Ekran Sarmalama)**

Oyuncunun veya asteroitlerin ekran dışına çıktığında karşı taraftan belirmesini sağlayan sistemdir.

* `Camera.main.ScreenToWorldPoint` ile ekran sınırları (Viewport/World Bounds) hesaplanır.

* Nesne sağ sınıra ulaştığında x koordinatı sol sınıra, üst sınıra ulaştığında y koordinatı alt sınıra taşınır.

#### **`MoveController.cs`**

* Nesneye ivmeli hareket kazandırır.

* Uzay boşluğundaki sürtünmesizlik hissini taklit etmek için `Rigidbody2D.AddForce` ve sürükleme (Drag) değerlerini işler.

### 4. Oyuncu ve Aktörler

#### **`PlayerController.cs`**

* Girdileri `PlayerInput` üzerinden alır.

* İtiş motorları çalıştığında `AnimationController` aracılığıyla `Thruster` animasyonunu aktif eder ve ses motoruna itiş efekti komutu gönderir.

* Vurulma durumunda oyuncunun yeniden doğma (Respawn) ve geçici dokunulmazlık (Invulnerability) durumunu yönetir.

#### **`AsteroidController.cs`**

* Asteroit boyutu $3$ kademeden oluşur: **Büyük (Big)**, **Orta (Mid)**, **Küçük (Small)**.

* Büyük asteroit vurulduğunda 2 adet Orta asteroide bölünür.

* Orta asteroit vurulduğunda 2 adet Küçük asteroide bölünür.

* Küçük asteroit vurulduğunda patlama efektiyle tamamen yok olur.

#### **`EnemyShipController.cs`**

* Düşman gemisi ekran boyunca ilerlerken düzenli aralıklarla oyuncunun pozisyonuna doğru mermi fırlatır.

* Açı hesaplaması:
  

  $$
  \theta = \arctan2(y_{player} - y_{enemy}, x_{player} - x_{enemy})
  $$

## 📐 Fizik ve Matematiksel Mantık

### Ekran Sarmalama (Screen Wrap) Formülü

Nesnenin $P(x, y)$ konumunun ekran sınırları $Bounds(X_{min}, X_{max}, Y_{min}, Y_{max})$ dışına çıkma durumu:

$$
x_{yeni} = \begin{cases} X_{min}, & x > X_{max} \\ X_{max}, & x < X_{min} \\ x, & \text{diğer} \end{cases}
$$

$$
y_{yeni} = \begin{cases} Y_{min}, & y > Y_{max} \\ Y_{max}, & y < Y_{min} \\ y, & \text{diğer} \end{cases}
$$

### İtiş ve Vektörel Hız Hesaplaması

Oyuncu ileri yön tuşuna bastığında uygulanan kuvvet:

$$
\vec{F} = \vec{d} \cdot F_{thrust}
$$

Burada $\vec{d} = (\cos(\theta), \sin(\theta))$ oyuncunun baktığı yön vektörüdür.

## 📊 Skor ve Oyuncu Denge Tablosu

| Nesne Türü | Boyut / Tip | Puan Değeri | Bölünme Sayısı | 
 | ----- | ----- | ----- | ----- | 
| **Büyük Asteroit** | Big (V1-V3) | 20 Puan | 2 Orta Asteroit | 
| **Orta Asteroit** | Mid (V1-V3) | 50 Puan | 2 Küçük Asteroit | 
| **Küçük Asteroit** | Small (V1-V3) | 100 Puan | Yok (Yok olur) | 
| **Düşman Gemisi** | Saucer | 200 Puan | Yok | 

## 🎮 Kurulum ve Derleme (Build) Adımları

### Gereksinimler

* **Unity Version**: `2021.3.x LTS` veya daha yeni bir sürüm.

* **Render Pipeline**: Universal Render Pipeline (URP) veya Built-in 2D Render.

### Çalıştırma Adımları

1. Repoyu bilgisayarınıza klonlayın:

   ```
   git clone https://github.com/enes-sen/asteroidsclone.git
   
   ```

2. **Unity Hub** uygulamasını açın ve `Add project from disk` seçeneğiyle proje klasörünü seçin.

3. Proje açıldıktan sonra `Assets/_GameAssets/Scenes/` dizininde bulunan **`MainMenu.unity`** sahnesini çift tıklayarak açın.

4. Editörün üst kısmındaki **Play** butonuna basarak oyunu başlatın.

### Kontroller

* **W / Yukarı Ok**: İleri İtiş Gücü (Thrust)

* **A-D / Sol-Sağ Ok**: Gemi Dönüşü (Rotation)

* **Space (Boşluk)**: Lazer Ateşi (Fire)

* **Escape**: Oyunu Duraklatma / Menü

```
eof

```
