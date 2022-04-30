using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using haberportalı.Models;
using haberportalı.ViewModel;

namespace haberportalı.Controllers
{
    public class ServisController : ApiController
    {
        haberportalıEntities db = new haberportalıEntities();
        sonucModel sonuc = new sonucModel();

        #region haberler

        [HttpGet]
        [Route("api/haberliste")]
        public List<haberModel> HaberListe()
        {
            List<haberModel> liste = db.haberler.Select(x => new haberModel()
            {
                haber_Id = x.haber_Id,
                haber_adi = x.haber_adi,
                haber_katid = x.haber_katid,
            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/haberbyid/{haber_Id}")]
        public haberModel haberById(string haber_Id)
        {
            haberModel kayit = db.haberler.Where(s => s.haber_Id == haber_Id).Select(x => new
                haberModel()
            {
                haber_Id = x.haber_Id,
                haber_adi = x.haber_adi,
                haber_katid = x.haber_katid,
            }
            ).FirstOrDefault();

            return kayit;
        }

        [HttpPost]
        [Route("api/haberekle")]
        public sonucModel haberEkle(haberModel model)
        {
            if (db.haberler.Count(s => s.haber_adi == model.haber_adi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Haber Kayıtlıdır!";
                return sonuc;
            }
            haberler yeni = new haberler();
            yeni.haber_Id = Guid.NewGuid().ToString();
            yeni.haber_adi = model.haber_adi;
            yeni.haber_katid = model.haber_katid;
            db.haberler.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Haber Eklendi";


            return sonuc;
        }
        [HttpPut]
        [Route("api/haberduzenle")]
        public sonucModel haberDuzenle(haberModel model)
        {
            haberler kayit = db.haberler.Where(s => s.haber_Id == model.haber_Id).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            kayit.haber_adi = model.haber_adi;
            kayit.haber_katid = model.haber_katid;

            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Haber Düzenlendi";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/habersil/{haber_Id}")]
        public sonucModel haberSil(string haber_Id)
        {
            haberler kayit = db.haberler.Where(s => s.haber_Id == haber_Id).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            db.haberler.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Haber Silindi";

            return sonuc;
        }


        #endregion

        #region icerik

        [HttpGet]
        [Route("api/icerikliste")]
        public List<icerikModel> İcerikListe()
        {
            List<icerikModel> liste = db.icerik.Select(x => new icerikModel()
            {
                icerk_Id = x.icerk_Id,
                icerik_adi = x.icerik_adi,
                icerik_haberid = x.icerik_haberid,
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/icerikbyid/{icerk_Id}")]
        public icerikModel icerikById(string icerk_Id)
        {
            icerikModel kayit = db.icerik.Where(s => s.icerk_Id == icerk_Id).Select(x => new icerikModel()
            {
                icerk_Id = x.icerk_Id,
                icerik_adi = x.icerik_adi,
                icerik_haberid = x.icerik_haberid,
            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/icerikekle")]
        public sonucModel icerikEkle(icerikModel model)
        {

            icerik yeni = new icerik();
            yeni.icerk_Id = Guid.NewGuid().ToString();
            yeni.icerik_adi = model.icerik_adi;
            yeni.icerik_haberid = model.icerik_haberid;
            db.icerik.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "İçerik Eklendi";
            return sonuc;
        }
        [HttpPut]
        [Route("api/icerikduzenle")]
        public sonucModel icerikDuzenle(icerikModel model)
        {
            icerik kayit = db.icerik.Where(s => s.icerk_Id == model.icerk_Id).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunmadı!";
                return sonuc;
            }

            kayit.icerk_Id = model.icerk_Id;
            kayit.icerik_adi = model.icerik_adi;
            kayit.icerik_haberid = model.icerik_haberid;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "İçerik Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/iceriksil/{icerk_Id}")]
        public sonucModel icerikSil(string icerk_Id)
        {
            icerik kayit = db.icerik.Where(s => s.icerk_Id == icerk_Id).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunmadı!";
                return sonuc;
            }

            db.icerik.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "icerik Silndi";
            return sonuc;
        }

        #endregion

        #region kategori

        [HttpGet]
        [Route("api/kategoriliste")]
        public List<katModel> KategoriListe()
        {
            List<katModel> liste = db.kategori.Select(x => new katModel()
            {
                kat_Id = x.kat_Id,
                kat_adi = x.kat_adi,
            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/kategoribyid/{kat_Id}")]
        public katModel kategoriById(string kat_Id)
        {
            katModel kayit = db.kategori.Where(s => s.kat_Id == kat_Id).Select(x => new
                katModel()
            {
                kat_Id = x.kat_Id,
                kat_adi = x.kat_adi,
            }
            ).FirstOrDefault();

            return kayit;
        }

        [HttpPost]
        [Route("api/kategoriekle")]
        public sonucModel kategoriEkle(katModel model)
        {
            if (db.kategori.Count(s => s.kat_adi == model.kat_adi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Kategori Kayıtlıdır!";
                return sonuc;
            }
            kategori yeni = new kategori();
            yeni.kat_Id = Guid.NewGuid().ToString();
            yeni.kat_adi = model.kat_adi;
            db.kategori.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Eklendi";


            return sonuc;
        }
        [HttpPut]
        [Route("api/kategoriduzenle")]
        public sonucModel kategoriDuzenle(katModel model)
        {
            kategori kayit = db.kategori.Where(s => s.kat_Id == model.kat_Id).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            kayit.kat_adi = model.kat_adi;

            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Düzenlendi";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/kategorisil/{kat_Id}")]
        public sonucModel kategoriSil(string kat_Id)
        {
            kategori kayit = db.kategori.Where(s => s.kat_Id == kat_Id).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            db.kategori.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Silindi";

            return sonuc;
        }

        #endregion

    }
}
