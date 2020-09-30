using App.Controllers.Resourses;
using App.Features;
using App.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            //Domain to Resource
            CreateMap<Make, MakeResourse>();
            CreateMap<Make, KeyValuePairResource>();
            CreateMap<Model, KeyValuePairResource>();
            CreateMap<Feature, KeyValuePairResource>();
            CreateMap<Vehicle, SaveVehicleResource>()
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource { Name = v.ContactName, Phone = v.ContactPhone, Email = v.ContactEmail }))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.VehicleFeatures.Select(vf => vf.FeatureID)));
            CreateMap<Vehicle, VehicleResource>()
                .ForMember(vr => vr.Make, opt => opt.MapFrom(v => v.Model.Make))
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource { Name = v.ContactName, Phone = v.ContactPhone, Email = v.ContactEmail }))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.VehicleFeatures.Select(vf => new KeyValuePairResource{ ID = vf.Feature.ID, Name = vf.Feature.Name})));

            //Resource to domain
            CreateMap<SaveVehicleResource, Vehicle>()
              .ForMember(v => v.ID, opt => opt.Ignore())
              .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
              .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
              .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
              .ForMember(v => v.VehicleFeatures, opt => opt.Ignore())
              .AfterMap((vr, v) => {
                  //remove unselected features
                  var removedFeatures = v.VehicleFeatures.Where(f => !vr.Features.Contains(f.FeatureID));
                  foreach (var f in removedFeatures.ToList())
                      v.VehicleFeatures.Remove(f);

                  //add new feature
                  var addedFeature = vr.Features.Where(id => !v.VehicleFeatures.Any(f => f.FeatureID == id))
                    .Select(id => new VehicleFeature { FeatureID = id });
                  foreach (var f in addedFeature)
                      v.VehicleFeatures.Add(f);
              });
        }
    }
}
