using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Venice.Models;

namespace Venice.Controllers
{
    public class HomeController : Controller
    {
        ParfumeriEntities baglanti = new ParfumeriEntities();

        // GET: Home
        public ActionResult Index()
        {
            ViewBag.SonEklenen = baglanti.Urunler.Take(6);
            ViewBag.Duyuru = baglanti.Duyurular.ToList();

            return View();
        }

        [HttpPost]
        public ActionResult Giris(FormCollection form)
        {
            List<Uyeler> list = baglanti.Uyeler.ToList();
            foreach (var uyelist in list)
            {
                if (uyelist.Email==form["Email"] && uyelist.Sifre==form["Sifre"])
                {
                    Session["Uye"] = uyelist.ID;
                }
            }

            return Redirect("~/Home"); ;
        }

        [HttpPost]
        public ActionResult Kayit(FormCollection form)
        {
            Uyeler Uye = new Uyeler();
            Uye.Ad = form["Ad"];
            Uye.Soyad=form["Soyad"];
            Uye.Email = form["Email"];
            Uye.Sifre = form["Sifre"];
            Uye.KayitTarihi = DateTime.Now;
            baglanti.Uyeler.Add(Uye);
            baglanti.SaveChanges();

            return Redirect("~/Home");
        }


        public ActionResult List()
        {
            ViewBag.List = baglanti.Urunler.ToList();



            return View();
        }


        public ActionResult Urun(int id)
        {

            int uyeid = Convert.ToInt32(Session["Uye"]);
            ViewBag.Urun = baglanti.Urunler.Find(id);
            ViewBag.KullaniciUye = baglanti.Uyeler.Find(uyeid);
            ViewBag.Yorumtablo = baglanti.YorumTablo.Where(x=>x.UrunID==id);
            ViewBag.Yorumlar = baglanti.Yorumlar.ToList();
            ViewBag.Uye = baglanti.Uyeler.ToList();


            return View();
        }

        [HttpPost]
        public ActionResult Yorum(FormCollection form)
        {
          
            Yorumlar yorum = new Yorumlar();
            yorum.KonuIcerik = form["Yorum"];
            yorum.KonuBaslik = "a";
            baglanti.Yorumlar.Add(yorum);
            baglanti.SaveChanges();
      
          
          
            Yorumlar yorumid = new Yorumlar();

            yorumid = baglanti.Yorumlar.OrderByDescending(x => x.YorumID).Take(1).FirstOrDefault();

            int urunidisi = Convert.ToInt32(form["urunıd"]);

            YorumTablo yorumiliski = new YorumTablo();
            yorumiliski.UrunID =urunidisi;
            yorumiliski.UyeID = Convert.ToInt32(Session["Uye"]);
            yorumiliski.YorumID = yorumid.YorumID;
            yorumiliski.Tarih = DateTime.Now;
            baglanti.YorumTablo.Add(yorumiliski);
            baglanti.SaveChanges();

            return Redirect("~/Home/Urun/"+urunidisi);
        }
    }
}