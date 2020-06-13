using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using O2.Services.Certificates.API.Models;
using O2.Services.Certificates.Business.Models;

namespace O2.Services.Certificates.API.Mappings
{
    public static class CertificateMappings
    {
        public static CertificateViewModel ToViewModel(this Certificate model)
        {
            return model != null ? new CertificateViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                ShortNumber = model.ShortNumber,
                Serial = model.Serial,
                Number = model.Number,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Middlename = model.Middlename,
                DateOfCert = model.DateOfCert,
                Education = model.Education,
                Lock = model.Lock,
                Visible = model.Visible,
                LockInfo = model.LockInfo
            } : null;
        }

        public static Certificate ToServiceModel(this CertificateViewModel model)
        {
            return model != null ? new Certificate()
            {
                Id = model.Id,
                Name = model.Name,
                ShortNumber = model.ShortNumber,
                Serial = model.Serial,
                Number = model.Number,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Middlename = model.Middlename,
                DateOfCert = model.DateOfCert,
                Education = model.Education,
                Lock = model.Lock,
                Visible = model.Visible,
                LockInfo = model.LockInfo
            } : null;
        }

        public static IReadOnlyCollection<CertificateViewModel> ToViewModel(this IReadOnlyCollection<Certificate> models)
        {
            if (models.Count == 0)
                return Array.Empty<CertificateViewModel>();
            
            var certificates = new CertificateViewModel[models.Count];
            var i = 0;
            foreach (var model in models)
            {
                certificates[i] = model.ToViewModel();
                ++i;
            }
            return new ReadOnlyCollection<CertificateViewModel>(certificates);
        }
    }
}