using System.Collections.Generic;

namespace ScholarshipManagementSystem.Constants
{
public static class Permissions
{
    public static List<string> GeneratePermissionsForModule(string module)
    {
        return new List<string>()
        {
            $"Permissions.{module}.Create",
            $"Permissions.{module}.View",
            $"Permissions.{module}.Edit",
            $"Permissions.{module}.Delete",
        };
    }

    public static class Country
    {
        public const string View = "Permissions.Country.View";
        public const string Create = "Permissions.Country.Create";
        public const string Edit = "Permissions.Country.Edit";
        public const string Delete = "Permissions.Country.Delete";
    }

    public static class Division
    {
        public const string View = "Permissions.Division.View";
        public const string Create = "Permissions.Division.Create";
        public const string Edit = "Permissions.Division.Edit";        
        public const string Delete = "Permissions.Division.Delete";        
    }
    public static class District
    {
        public const string View = "Permissions.District.View";
        public const string Create = "Permissions.District.Create";
        public const string Edit = "Permissions.District.Edit";        
        public const string Delete = "Permissions.District.Delete";        
    }
        public static class DistrictDetail
        {
            public const string View = "Permissions.DistrictDetail.View";
            public const string Create = "Permissions.DistrictDetail.Create";
            public const string Edit = "Permissions.DistrictDetail.Edit";
            public const string Delete = "Permissions.DistrictDetail.Delete";
        }
        public static class ScholarshipType
    {
        public const string View = "Permissions.ScholarshipType.View";
        public const string Create = "Permissions.ScholarshipType.Create";
        public const string Edit = "Permissions.ScholarshipType.Edit";
        public const string Delete = "Permissions.ScholarshipType.Delete";
    }
    public static class ScholarshipFiscalYear
    {
        public const string View = "Permissions.ScholarshipFiscalYear.View";
        public const string Create = "Permissions.ScholarshipFiscalYear.Create";
        public const string Edit = "Permissions.ScholarshipFiscalYear.Edit";
        public const string Delete = "Permissions.ScholarshipFiscalYear.Delete";
    }
        public static class Degree
        {
            public const string View = "Permissions.Degree.View";
            public const string Create = "Permissions.Degree.Create";
            public const string Edit = "Permissions.Degree.Edit";
            public const string Delete = "Permissions.Degree.Delete";
        }
        public static class Discipline
        {
            public const string View = "Permissions.Discipline.View";
            public const string Create = "Permissions.Discipline.Create";
            public const string Edit = "Permissions.Discipline.Edit";
            public const string Delete = "Permissions.Discipline.Delete";
        }
        public static class InstituteType
        {
            public const string View = "Permissions.InstituteType.View";
            public const string Create = "Permissions.InstituteType.Create";
            public const string Edit = "Permissions.InstituteType.Edit";
            public const string Delete = "Permissions.InstituteType.Delete";
        }
        public static class Institute
        {
            public const string View = "Permissions.Institute.View";
            public const string Create = "Permissions.Institute.Create";
            public const string Edit = "Permissions.Institute.Edit";
            public const string Delete = "Permissions.Institute.Delete";
        }
        public static class InstituteDepartment
        {
            public const string View = "Permissions.InstituteDepartment.View";
            public const string Create = "Permissions.InstituteDepartment.Create";
            public const string Edit = "Permissions.InstituteDepartment.Edit";
            public const string Delete = "Permissions.InstituteDepartment.Delete";
        }
        public static class QualificationLevel
        {
            public const string View = "Permissions.QualificationLevel.View";
            public const string Create = "Permissions.QualificationLevel.Create";
            public const string Edit = "Permissions.QualificationLevel.Edit";
            public const string Delete = "Permissions.QualificationLevel.Delete";
        }
        public static class Scheme
        {
            public const string View = "Permissions.Scheme.View";
            public const string Create = "Permissions.Scheme.Create";
            public const string Edit = "Permissions.Scheme.Edit";
            public const string Delete = "Permissions.Scheme.Delete";
        }
        public static class SchemeLevel
        {
            public const string View = "Permissions.SchemeLevel.View";
            public const string Create = "Permissions.SchemeLevel.Create";
            public const string Edit = "Permissions.SchemeLevel.Edit";
            public const string Delete = "Permissions.SchemeLevel.Delete";
        }
        public static class PolicySRCForum
        {
            public const string View = "Permissions.PolicySRCForum.View";
            public const string Create = "Permissions.PolicySRCForum.Create";
            public const string Edit = "Permissions.PolicySRCForum.Edit";
            public const string Delete = "Permissions.PolicySRCForum.Delete";
        }
        public static class DistrictQoutaBySchemeLevel
        {
            public const string View = "Permissions.DistrictQoutaBySchemeLevel.View";
            public const string Create = "Permissions.DistrictQoutaBySchemeLevel.Create";
            public const string Edit = "Permissions.DistrictQoutaBySchemeLevel.Edit";
            public const string Delete = "Permissions.DistrictQoutaBySchemeLevel.Delete";
        }   
    }
}