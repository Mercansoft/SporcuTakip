using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using Spor.Core.Entity;
using Spor.Core.Context;

namespace Spor.Bussines
{
    public class BL_Kullanici
    {
       
        private void _KullaniciRolEkle(string RolAdi, string KullaniAdi)
        {
            Roles.AddUserToRole(KullaniAdi, RolAdi);
        }
        private void _EkleDetay(Kullanici k,string LoginID)
        {
            MembershipUser user = Membership.GetUser(k.KullaniciAdi);
            string guid = user.ProviderUserKey.ToString();
            
            Kullanici model = new Kullanici();
            model.Resim = k.Resim;
            model.Sifre = k.Sifre;
            model.GizliCevap = k.GizliCevap;
            model.GizliSoru = k.GizliSoru;
            model.Tarih = Convert.ToDateTime(user.CreationDate);
            model.DogumTarihi = Convert.ToDateTime(k.DogumTarihi);
            model.Adres = k.Adres;
            model.AdSoyad = k.AdSoyad;
            model.Telefon = k.Telefon;
            model.Email = k.Email;
            model.AdminName = guid;
            model.KullaniciAdi = k.KullaniciAdi;
            model.AdminName = LoginID;
            model.Onay = true;


            using (MyDbContext db = new MyDbContext())
            {

                db.Kullanicilar.Add(model);
                db.SaveChanges();
                user.IsApproved = true;
                Membership.UpdateUser(user);
            }
        }
        public string _Ekle(Kullanici k, string RolID,string LoginID)
        {
            string mesaj = string.Empty;
            MembershipCreateStatus durum;
            Membership.CreateUser(k.KullaniciAdi, k.Sifre, k.Email, k.GizliSoru, k.GizliCevap, false, out durum);
            switch (durum)
            {
                case MembershipCreateStatus.Success:
                    _EkleDetay(k, LoginID);
                    _KullaniciRolEkle(RolID, k.KullaniciAdi);
                    mesaj += "Başarıyla Kayıt Edildi.<br>";
                    break;
                case MembershipCreateStatus.InvalidUserName:
                    mesaj += "geçersiz kullanıcı adı hatası<br>";
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    mesaj += "geçersiz şifre hatası<br>";
                    break;
                case MembershipCreateStatus.InvalidQuestion:
                    mesaj += "geçersiz soru hatası<br>";
                    break;
                case MembershipCreateStatus.InvalidAnswer:
                    mesaj += "geçersiz cevap hatası<br>";
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    mesaj += "geçersiz email hatası<br>";
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    mesaj += "Kullanılmışi Kullanıcı adı hatası<br>";
                    break;
                case MembershipCreateStatus.DuplicateEmail:
                    mesaj += "Yazdığınız E-Mail adresi daha önce başka bir kullanıcı için kullanılmakadır.<br>";
                    break;
                case MembershipCreateStatus.UserRejected:
                    mesaj += "engellenmiş kullnaıcı<br>";
                    break;
                case MembershipCreateStatus.InvalidProviderUserKey:
                    mesaj += "geçersiz kullanıcı key hatası<br>";
                    break;
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    mesaj += "Kullanılmış Kullanıcı Key Hatası<br>";
                    break;
                case MembershipCreateStatus.ProviderError:
                    mesaj += "Üye Yönetimi sağlayıcısı hatası<br>";
                    break;
                default:
                    break;
            }
            return mesaj;
        }
        public string _Duzenle(Kullanici k,string YeniRol,string EskiRol)
        {
            string durum = "";
            MyDbContext db = new MyDbContext();

            MembershipUser user = Membership.GetUser(k.KullaniciAdi);
            
            bool pwdurum = user.ChangePassword(user.ResetPassword(), k.Sifre);
            user.Email = k.Email;
            user.IsApproved = k.Onay;

            Kullanici model = db.Kullanicilar.Where(x => x.KullaniciAdi == user.UserName).SingleOrDefault();

            model.Adres = k.Adres;
            model.AdSoyad = k.AdSoyad;
            model.Email = k.Email;
            model.DogumTarihi=Convert.ToDateTime(k.DogumTarihi);
            model.KullaniciAdi = k.KullaniciAdi;
            model.Telefon = k.Telefon;
            model.GizliCevap = k.GizliCevap;
            model.GizliSoru = k.GizliSoru;
            model.Sifre = k.Sifre;
            model.Onay = k.Onay;

            try
            {
                Membership.UpdateUser(user);
                Roles.RemoveUserFromRole(k.KullaniciAdi, EskiRol);
                Roles.AddUserToRole(k.KullaniciAdi, YeniRol);
                db.SaveChanges();
                durum = "Başarıyla Güncellenmiştir.";
            }
            catch (Exception)
            {
                durum = "Güncelleme Hatası.";
            }
            return durum;
        }
        private void _MusteriDogrulamaMail(string username)
        {
            Ayar ayars = _Ayarlar();
            MembershipUser user = Membership.GetUser(username);
            string ConfirmationGuid = user.ProviderUserKey.ToString();
            string verifyUrl = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) +
                               "/Musteri/Verify?Id=" +
                               ConfirmationGuid;

            string bodyMessage = string.Format("üyeliğiniz başarıyla oluşturulmuştur. Aşağıdaki linke tıkladığınızda hesabınızın aktif olacaktır.\n");
            bodyMessage += verifyUrl;

            var message = new System.Net.Mail.MailMessage(ayars.email, user.Email)
            {
                Subject = "Üyeliğinizi doğrulayın.",
                Body = bodyMessage
            };

            var client = new System.Net.Mail.SmtpClient();
            client.Send(message);

        }
        private Ayar _Ayarlar()
        {
            using (MyDbContext db = new MyDbContext())
            {
                Ayar model = db.Ayarlar.FirstOrDefault();
                return model;
            }

        }
    }
}
