public interface IEnemyMoveBehaviour
{
    void Init(EnemyController controller);
    void Tick();

}

public interface IHurtBehaviour
{
    void Init(EnemyController controller);
    void OnDamaged(int damage); // 事件入口：被打的瞬间
    void Tick();                // 每帧：闪烁恢复/无敌帧倒计时等
}