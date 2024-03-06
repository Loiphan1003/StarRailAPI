namespace StarRailAPI.Common
{
    public static class Errors
    {
        public static Error AlreadyName(string name) => new Error(
            "AlreadyName", $"The {name} has already create"
        );

        public static Error NotFound(string name, string type) => new Error(
            "NotFound", $"The {type} {name} not found"
        );

        public static Error UploadFileFailed(string message) => new Error(
            "UploadLoadFile", $"{message}"
        );
    }
}