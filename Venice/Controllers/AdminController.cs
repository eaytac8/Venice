using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Venice.Models;

namespace Venice.Controllers
{
    public class AdminController : Controller
    {
        ParfumeriEntities baglanti = new ParfumeriEntities();


        // GET: Admin
        public ActionResult Index()
        {
            List<Urunler> urun = baglanti.Urunler.ToList();

            return View(urun);
        }

        public ActionResult ResimEkle()
        {
            ViewBag.id = Convert.ToInt32(RouteData.Values["ID"]);

            return View();
        }
        [HttpPost]
        public ActionResult ResimEkle(HttpPostedFileBase resim,FormCollection idsi)
        {
            Random rand = new Random();
            Urunler urun = new Urunler();

            int id = Convert.ToInt32(idsi["id"]);

            var resimuzantisorgu = Path.GetFileNameWithoutExtension(resim.ContentType).ToLower();//uzantısı alınır

            var resimadi = Path.GetFileName(resim.FileName);//adı alındı
            var resimuzanti = Path.GetFileNameWithoutExtension(resim.ContentType).ToLower();//uzantısı alınır
            string rastgeleSayi = rand.Next(100000000, 999999999).ToString();
            resimadi = rastgeleSayi + DateTime.Now.Second.ToString() + "id"+ "." + resimuzanti.ToString();//rasgele isim atıldı ve uzantı konuldu
            var kayityeriveadi = Path.Combine(Server.MapPath("/Tamplate/img/Urun/"), resimadi);//KAYDEDİLECEĞİ YER
            resim.SaveAs(kayityeriveadi);

            
          
            Urunler guncelleme = baglanti.Urunler.Where(x => x.ID == id).FirstOrDefault();
            guncelleme.Resim = "/Tamplate/img/Urun/" + resimadi;          
            baglanti.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult UrunEkle()
        {
            ViewBag.MarkaID = new SelectList(baglanti.Marka, "MarkaID", "MarkaAdi");
            ViewBag.CinsiyetID = new SelectList(baglanti.Cinsiyet, "CinsiyetID", "Cinsiyeti");
            ViewBag.TurID = new SelectList(baglanti.Tur, "TurID", "Turu");
            ViewBag.BoyutID = new SelectList(baglanti.Boyut, "BoyutID", "Boyutu");
            ViewBag.AromaID = new SelectList(baglanti.Aroma, "AromaID", "Adi");
            UrunViewModel model = new Models.UrunViewModel();

            return View(model);
        }
        [HttpPost]
        public ActionResult UrunEkle(UrunViewModel model)
        {
            model.urun.EklenmeTarihi = DateTime.Now;
            baglanti.Urunler.Add(model.urun);
            baglanti.SaveChanges();

            AromaUrun yeniaroma = new Models.AromaUrun();
            yeniaroma.UrunID = model.urun.ID;
            yeniaroma.AromaID = model.aroma.AromaID;
            baglanti.AromaUrun.Add(yeniaroma);
            baglanti.SaveChanges();
            return RedirectToAction("Index");
        } 

        public ActionResult UrunSil()
        {
            int id = Convert.ToInt32(RouteData.Values["ID"]);
            foreach (var item in baglanti.AromaUrun.Where(x=>x.UrunID==id))
            {
                baglanti.AromaUrun.Remove(item);
            }
            baglanti.Urunler.Remove(baglanti.Urunler.Find(id));
            baglanti.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult CinsiyetEkle(FormCollection form)
        {
            Cinsiyet cinsiyet = new Cinsiyet();
            cinsiyet.Cinsiyeti = form["Cinsiyet"];
            baglanti.Cinsiyet.Add(cinsiyet);
            baglanti.SaveChanges();

            return Redirect("~/Admin");
        }
        public ActionResult MarkaEkle(FormCollection form)
        {
            Marka marka = new Marka();
            marka.MarkaAdi = form["Marka"];
            baglanti.Marka.Add(marka);
            baglanti.SaveChanges();

            return Redirect("~/Admin");
        }

        public ActionResult TurEkle(FormCollection form)
        {
            Tur tur = new Tur();
            tur.Turu = form["Tur"];
            baglanti.Tur.Add(tur);
            baglanti.SaveChanges();

            return Redirect("~/Admin");
        }

        public ActionResult BoyutEkle(FormCollection form)
        {
            Boyut boyut = new Boyut();
            boyut.Boyutu = form["Boyut"];
            baglanti.Boyut.Add(boyut);
            baglanti.SaveChanges();

            return Redirect("~/Admin");
        }

        public ActionResult AromaEkle(FormCollection form)
        {
            Aroma aroma = new Aroma();
            aroma.Adi = form["Aroma"];
            baglanti.Aroma.Add(aroma);
            baglanti.SaveChanges();

            return Redirect("~/Admin");
        }

        public ActionResult DuyuruEkle(FormCollection form)
        {
            Duyurular duyuru = new Duyurular();
            duyuru.DuyuruBasligi = form["DuyuruBaslik"];
            duyuru.Duyuru= form["DuyuruIcerik"];
            baglanti.Duyurular.Add(duyuru);
            baglanti.SaveChanges();

            return Redirect("~/Admin");
        }


    }
}