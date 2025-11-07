using Microsoft.AspNetCore.Mvc;
using NousPainelAPI.Domain;

namespace NousPainelAPI.Utils
{
    public static class HateoasHelper
    {
        public static object GenerateLinks(Aluno a, IUrlHelper url)
        {
            var self = url.Action("Get", "Aluno", new { id = a.Id }, protocol: null);
            var update = url.Action("Update", "Aluno", new { id = a.Id }, protocol: null);
            var delete = url.Action("Delete", "Aluno", new { id = a.Id }, protocol: null);
            var checkin = url.Action("Checkin", "Aluno", new { id = a.Id }, protocol: null);

            return new
            {
                a.Id,
                a.Nome,
                a.Email,
                a.CriadoEm,
                Checkins = a.Checkins?.Select(c => new
                {
                    c.Id,
                    c.MoodScore,
                    c.Observacao,
                    c.CriadoEm
                }),
                _links = new
                {
                    self,
                    update,
                    delete,
                    checkin
                }
            };
        }
    }
}
