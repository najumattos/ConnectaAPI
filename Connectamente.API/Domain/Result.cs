namespace Connectamente.API.Domain;

    public record Result<T>(bool IsSuccess, T? Value, string? Error)
    {
        // Atalho para Sucesso
        public static Result<T> Success(T value) => new(true, value, null);

        // Atalho para Falha
        public static Result<T> Failure(string error) => new(false, default, error);
    }

    // Versão simplificada para métodos que não retornam dados (void)
    public record Result(bool IsSuccess, string? Error)
    {
        public static Result Success() => new(true, null);
        public static Result Failure(string error) => new(false, error);
    }