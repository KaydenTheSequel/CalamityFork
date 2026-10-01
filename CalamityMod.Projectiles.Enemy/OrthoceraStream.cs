using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Enemy;

public class OrthoceraStream : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Enemy";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 32;
		base.Projectile.height = 32;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 3;
		base.Projectile.extraUpdates = 2;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[1]++;
		if (base.Projectile.ai[1] >= 4f)
		{
			base.Projectile.tileCollide = true;
		}
		base.Projectile.scale -= 0.002f;
		if (base.Projectile.scale <= 0f)
		{
			base.Projectile.Kill();
		}
		if (base.Projectile.ai[0] <= 3f)
		{
			base.Projectile.ai[0]++;
			return;
		}
		base.Projectile.velocity.Y = base.Projectile.velocity.Y + 0.075f;
		for (int i = 0; i < 3; i++)
		{
			Vector2 positionDelta = base.Projectile.velocity / 3f * (float)i;
			int spawnDelta = 14;
			int dustIdx = Dust.NewDust(new Vector2(base.Projectile.position.X + (float)spawnDelta, base.Projectile.position.Y + (float)spawnDelta), base.Projectile.width - spawnDelta * 2, base.Projectile.height - spawnDelta * 2, 75, 0f, 0f, 100);
			Dust obj = Main.dust[dustIdx];
			obj.noGravity = true;
			obj.velocity *= 0.1f;
			obj.velocity += base.Projectile.velocity * 0.5f;
			obj.position -= positionDelta;
		}
		if (Main.rand.NextBool(8))
		{
			int spawnDelta2 = 16;
			int dustIdx2 = Dust.NewDust(new Vector2(base.Projectile.position.X + (float)spawnDelta2, base.Projectile.position.Y + (float)spawnDelta2), base.Projectile.width - spawnDelta2 * 2, base.Projectile.height - spawnDelta2 * 2, 75, 0f, 0f, 100, default(Color), 0.5f);
			Dust obj2 = Main.dust[dustIdx2];
			obj2.velocity *= 0.25f;
			Dust obj3 = Main.dust[dustIdx2];
			obj3.velocity += base.Projectile.velocity * 0.5f;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<Irradiated>(), 60);
		}
	}
}
