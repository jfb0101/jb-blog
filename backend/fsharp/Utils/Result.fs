namespace FsharpBackend

type Result<'R,'E> =
    | Success of result:'R
    | Error of error: 'E