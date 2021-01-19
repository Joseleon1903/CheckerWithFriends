using Assets.Scripts.WebSocket.Message;

namespace Assets.Scripts.Utils
{
    public class MessageUtils
    {

        public static ChessEndValidateCheckMoveReq ConvertFromMessage(ChessValidateCheckMoveResp resp, bool isvalid) {

            string isValidString = (isvalid) ? EnumHelper.TRUE : EnumHelper.FALSE;
            ChessEndValidateCheckMoveReq response = new ChessEndValidateCheckMoveReq(resp.lobbyCode, resp.gameType, isValidString, resp.playerCheck, 
                resp.startPosX, resp.startPosY, resp.endPosX, resp.endPosY);

            return response;
        }

        public static DataMessageResp ConvertFromMessageData(ChessValidateCheckMoveResp resp)
        {
            DataMessageResp data = new DataMessageResp();
            data.origin = resp.origin;
            data.operationCode = resp.operationCode;
            data.lobbyCode = resp.lobbyCode;
            data.gameType = resp.gameType;
            data.checketdMove = EnumHelper.FALSE;
            data.startPosX = resp.startPosX.ToString();
            data.startPosY = resp.startPosY.ToString();
            data.endPosX = resp.endPosX.ToString();
            data.endPosY = resp.endPosY.ToString();
            return data;
        }

    }
}