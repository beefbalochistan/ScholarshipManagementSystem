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
            $"Permissions.{module}.Analyzer",
            $"Permissions.{module}.Import",
        };
    }

 
    public static class Division
    {
        public const string View = "Permissions.Division.View";
        public const string Create = "Permissions.Division.Create";
        public const string Edit = "Permissions.Division.Edit";        
        public const string Delete = "Permissions.Division.Delete";        
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
        public static class ResultUploadType
        {
            public const string View = "Permissions.ResultUploadType.View";
            public const string Create = "Permissions.ResultUploadType.Create";
            public const string Edit = "Permissions.ResultUploadType.Edit";
            public const string Delete = "Permissions.ResultUploadType.Delete";
        }

        public static class Applicant
        {
            public const string View = "Permissions.Applicant.View";
            public const string Create = "Permissions.Applicant.Create";
            public const string Edit = "Permissions.Applicant.Edit";
            public const string Delete = "Permissions.Applicant.Delete";
        }
        public static class FormCollection
        {
            public const string View = "Permissions.FormCollection.View";            
        }
        public static class FormEntry
        {
            public const string View = "Permissions.FormEntry.View";
        }
        public static class SuperUser
        {
            public const string View = "Permissions.SuperUser.View";            
        }
        public static class ViewRejected
        {
            public const string View = "Permissions.ViewRejected.View";
        }       
        public static class ViewWaiting
        {
            public const string View = "Permissions.ViewWaiting.View";
        }
        public static class ViewISRC
        {
            public const string View = "Permissions.ViewISRC.View";
        }
        public static class ViewIA
        {
            public const string View = "Permissions.ViewIA.View";
        }
        public static class ViewFinding
        {
            public const string View = "Permissions.ViewFinding.View";
        }
        public static class TrackApplicant
        {
            public const string View = "Permissions.TrackApplicant.View";
        }       
        public static class Result
        {
            public const string View = "Permissions.Result.View";
            public const string Analyzer = "Permissions.Result.Analyzer";
            public const string Import = "Permissions.Result.Import";
        }
        public static class SMS
        {
            public const string View = "Permissions.SMS.View";                      
        }
        public static class Attachment
        {
            public const string View = "Permissions.Attachment.View";            
        }
        public static class MainInbox
        {
            public const string View = "Permissions.MainInbox.View";
        }
        public static class PendInbox
        {
            public const string View = "Permissions.PendInbox.View";
        }
        public static class ReturnInbox
        {
            public const string View = "Permissions.ReturnInbox.View";
        }
        public static class FMInbox
        {
            public const string View = "Permissions.FMInbox.View";
        }
        public static class VDInbox
        {
            public const string View = "Permissions.VDInbox.View";
        }
    }
}