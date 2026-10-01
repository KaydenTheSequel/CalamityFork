using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class Crystalline2 : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/Crystalline";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 20);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 30;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.localAI[0]++;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
		if (base.Projectile.localAI[0] != 10f || base.Projectile.ai[1] != 1f)
		{
			return;
		}
		int numProj = 2;
		float rotation = MathHelper.ToRadians(50f);
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int i = 0; i < numProj + 1; i++)
			{
				Vector2 perturbedSpeed = Utils.RotatedBy(new Vector2(base.Projectile.velocity.X * 0.8f, base.Projectile.velocity.Y * 0.8f), (double)MathHelper.Lerp(0f - rotation, rotation, (float)(i / (numProj - 1))), default(Vector2));
				int proj = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, perturbedSpeed, ModContent.ProjectileType<Crystalline2>(), (int)((float)base.Projectile.damage * 0.5f), base.Projectile.knockBack, base.Projectile.owner, 0f, 2f);
				Main.projectile[proj].timeLeft = 20;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft == ((base.Projectile.ai[1] == 2f) ? 20 : 30))
		{
			return false;
		}
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 154, base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f);
		}
		if (!(base.Projectile.ai[1] >= 1f))
		{
			return;
		}
		Vector2 projspeed = default(Vector2);
		for (int i = 0; i < 3; i++)
		{
			((Vector2)(ref projspeed))._002Ector(Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f));
			int shard = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, projspeed, 90, (int)((float)base.Projectile.damage * 0.4f), 2f, base.Projectile.owner);
			if (shard.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[shard].DamageType = RogueDamageClass.Instance;
			}
		}
	}
}
