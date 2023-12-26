using BAIAPI_12.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BAIAPI_12.Controllers
{
    public class QLNSController : ApiController
    {
        public IHttpActionResult GetAllDSNVs()
        {
            IList<DSNVViewModel> dsnvs = null;
            using (var ctx = new QLNSEntities())
            {
                dsnvs = ctx.DSNVs.Include("DSNVDMPHONG")
                    .Select(s => new DSNVViewModel()
                    {
                        MaNV = s.MaNV,
                        HoTen = s.HoTen,
                        NgaySinh = s.NgaySinh,
                        GioiTinh = s.GioiTinh,
                        MaPhong = s.MaPhong,
                        MaChucVu = s.MaChucVu,
                        HeSoLuong = s.HeSoLuong
                    }).ToList<DSNVViewModel>();
            }
            if (dsnvs.Count == 0)
            {
                return NotFound();
            }
            return Ok(dsnvs);
        }
        public IHttpActionResult GetDSNV(string id)
        {
            DSNVViewModel dsnv = null;
            using (var ctx = new QLNSEntities())
            {
                dsnv = ctx.DSNVs.Include("DSNVDMPHONG")
                    .Where(s => s.MaNV == id)
                    .Select(s => new DSNVViewModel()
                    {
                        MaNV = s.MaNV,
                        HoTen = s.HoTen,
                        NgaySinh = s.NgaySinh,
                        GioiTinh = s.GioiTinh,
                        MaPhong = s.MaPhong,
                        MaChucVu = s.MaChucVu,
                        HeSoLuong = s.HeSoLuong
                    }).FirstOrDefault();

                if (dsnv == null)
                {
                    return NotFound();
                }

                return Ok(dsnv);
            }
        }
        public IHttpActionResult PostNewDSNV(DSNVViewModel nhanvien)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            using (var ctx = new QLNSEntities())
            {
                ctx.DSNVs.Add(new DSNV()
                {
                    MaNV = nhanvien.MaNV,
                    HoTen = nhanvien.HoTen,
                    NgaySinh = nhanvien.NgaySinh,
                    GioiTinh = nhanvien.GioiTinh,
                    MaPhong = nhanvien.MaPhong,
                    MaChucVu = nhanvien.MaChucVu,
                    HeSoLuong = nhanvien.HeSoLuong
                });
                ctx.SaveChanges();
            }
            return Ok();
        }

        public IHttpActionResult PutDSNV(DSNVViewModel nhanvien)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            using (var ctx = new QLNSEntities())
            {
                var nv = ctx.DSNVs.SingleOrDefault((v) => v.MaNV == nhanvien.MaNV);
                if (nv != null)
                {
                    nv.HoTen = nhanvien.HoTen;
                    nv.NgaySinh = nhanvien.NgaySinh;
                    nv.GioiTinh = nhanvien.GioiTinh;
                    nv.MaPhong = nhanvien.MaPhong;
                    nv.MaChucVu = nhanvien.MaChucVu;
                    nv.HeSoLuong = nhanvien.HeSoLuong;
                    ctx.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
        }

        public IHttpActionResult DeleteDSNVs(DSNVViewModel nhanvien)
        {
            DSNV dsnv = null;
            using (var ctx = new QLNSEntities())
            {
                dsnv = ctx.DSNVs.SingleOrDefault((v) => v.MaNV == nhanvien.MaNV);
                if (dsnv == null)
                {
                    return NotFound();
                }

                ctx.DSNVs.Remove(dsnv);
                ctx.SaveChanges();

                return Ok();
            }
        }
    }
}

