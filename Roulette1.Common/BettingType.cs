using System.ComponentModel;

namespace Roulette1
{
    public enum BettingType
    {
        None,

        [Description("번호가 새겨진 사각형 내에 칩스를 놓으며 선을 건드려서는 안됩니다.")]
        Straight,
        [Description("칩스를 두 번호 사이의 선 위에 놓습니다. (0과 00 포함)")]
        Split,
        [Description("칩스를 레이아웃을 가로 지르는 세가지 번호가 있는 선 위에 놓습니다.")]
        Street,
        [Description("네 가지 번호가 교차하는 중간선 위에 칩스를 놓습니다.")]
        Square,
        [Description("칩스를 1, 2, 3, 0, 00의 교차하는 모서리 지점에 놓습니다.")]
        FiveNumber,
        [Description("칩스를 두 개의 Street가 교차하는 선 위에 놓습니다.")]
        SixNumber,
        [Description("칩스를 레이아웃 하단에 위치한 세 개 공간 중에 선택하여 놓습니다. (이 베팅은 해당 공간의 상위 수직선상의 12개의 번호를 가리킵니다)")]
        Column,
        [Description("칩스를 “1st 12”, “2nd 12”, “3rd 12”라고 표시된 지점에 놓습니다. (“1st 12”는 1~12, “2nd 12”는 13~24, “3rd 12”는 25~36의 숫자를 말합니다.)")]
        Dozen,
        [Description("1부터 18까지는 “Low Number”이며 19부터 36까지는 “High Number” 입니다.")]
        HighLow,
        [Description("0과 00을 제외한 모든 번호는 짝수(Even)와 홀수(Odd)로 구분합니다.")]
        EvenOdd,
        [Description("0과 00을 제외한 모든 번호는 적색(Red)과 흑색(Black)으로 구분합니다.")]
        Color,
        [Description("2nd Dozen과 3rd Dozen 사이의 0과 00을 베팅한 것입니다.")]
        CourtesyLine,
    }

    //Split,
    //Square,
    //FiveNumber,
    //SixNumber,
    //CourtesyLine,

}
