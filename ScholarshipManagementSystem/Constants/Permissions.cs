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
        public static class InstitudeType
        {
            public const string View = "Permissions.InstitudeType.View";
            public const string Create = "Permissions.InstitudeType.Create";
            public const string Edit = "Permissions.InstitudeType.Edit";
            public const string Delete = "Permissions.InstitudeType.Delete";
        }
        public static class Institude
        {
            public const string View = "Permissions.Institude.View";
            public const string Create = "Permissions.Institude.Create";
            public const string Edit = "Permissions.Institude.Edit";
            public const string Delete = "Permissions.Institude.Delete";
        }
        public static class InstitudeDepartment
        {
            public const string View = "Permissions.InstitudeDepartment.View";
            public const string Create = "Permissions.InstitudeDepartment.Create";
            public const string Edit = "Permissions.InstitudeDepartment.Edit";
            public const string Delete = "Permissions.InstitudeDepartment.Delete";
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
    }
}