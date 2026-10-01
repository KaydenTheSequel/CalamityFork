using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class DesecratedWaterProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/DesecratedWater";

	public override void SetDefaults()
	{
		base.Projectile.width = 25;
		base.Projectile.height = 25;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.aiStyle = 2;
		base.Projectile.timeLeft = 300;
		base.AIType = 48;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Shatter, base.Projectile.position);
		Vector2 vector2 = default(Vector2);
		((Vector2)(ref vector2))._002Ector(20f, 20f);
		for (int index = 0; index < 10; index++)
		{
			Dust.NewDust(base.Projectile.Center - vector2 / 2f, (int)vector2.X, (int)vector2.Y, 191);
		}
		for (int i = 0; i < 20; i++)
		{
			int index2 = Dust.NewDust(base.Projectile.Center - vector2 / 2f, (int)vector2.X, (int)vector2.Y, 179, 0f, 0f, 100, default(Color), 2.5f);
			Main.dust[index2].noGravity = true;
			Dust obj = Main.dust[index2];
			obj.velocity *= 5f;
			int index3 = Dust.NewDust(base.Projectile.Center - vector2 / 2f, (int)vector2.X, (int)vector2.Y, 179, 0f, 0f, 100, default(Color), 1.5f);
			Dust obj2 = Main.dust[index3];
			obj2.velocity *= 3f;
		}
		int numProj = (base.Projectile.Calamity().stealthStrike ? 12 : 5);
		if (base.Projectile.owner == Main.myPlayer)
		{
			Vector2 bubbleVel = default(Vector2);
			for (int j = 0; j < numProj; j++)
			{
				((Vector2)(ref bubbleVel))._002Ector((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
				((Vector2)(ref bubbleVel)).Normalize();
				bubbleVel *= (float)Main.rand.Next(10, 201) * 0.01f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, bubbleVel.X, bubbleVel.Y, ModContent.ProjectileType<DesecratedBubble>(), (int)((double)base.Projectile.damage * 0.6), base.Projectile.knockBack * 0.6f, base.Projectile.owner, base.Projectile.Calamity().stealthStrike ? 1f : 0f, 1f);
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
