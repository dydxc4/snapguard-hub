namespace SnapGuard.Hub.Types.Enums;

public enum ErrorCode
{
    Fail = -1,
    Success = 0x00,
    BadRequest = 0x01,
    EmptyRequest = 0x02,
    UnrecognizedRequest = 0x03,
    NotAllowedRequest = 0x04,
    UnauthorizedRequest = 0x05,
}
