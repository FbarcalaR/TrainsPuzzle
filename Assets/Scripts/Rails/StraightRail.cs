public class StraightRail : Rail {
    public override bool CanMoveDown() {
        return transform.eulerAngles.z == 90 || transform.eulerAngles.z == 270;
    }

    public override bool CanMoveUp() {
        return CanMoveDown();
    }

    public override bool CanMoveLeft() {
        return transform.eulerAngles.z == 0 || transform.eulerAngles.z == 180;
    }

    public override bool CanMoveRight() {
        return CanMoveLeft();
    }
}
