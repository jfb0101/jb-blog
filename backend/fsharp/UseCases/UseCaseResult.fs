namespace FsharpBackend.UseCases

type UseCaseStatus =
    | Success
    | Error

type UseCaseResult<'R,'E> =
    | Success of result:'R
    | Error of error: 'E