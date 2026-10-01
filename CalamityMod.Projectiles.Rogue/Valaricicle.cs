using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class Valaricicle : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.timeLeft = 180;
		base.Projectile.aiStyle = 1;
		base.Projectile.coldDamage = true;
		base.Projectile.ArmorPenetration = 10;
	}

	public override bool? CanHitNPC(NPC target)
	{
		return base.Projectile.timeLeft < 150 && target.CanBeChasedBy(base.Projectile);
	}

	public override void AI()
	{
		if (base.Projectile.timeLeft < 150)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 600f, 9f, 20f);
			return;
		}
		base.Projectile.velocity.X *= 0.9995f;
		base.Projectile.velocity.Y += 0.01f;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item27, base.Projectile.position);
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 67, base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>((base.Projectile.ai[2] == 1f) ? "CalamityMod/Projectiles/Rogue/Valaricicle2" : Texture, (AssetRequestMode)2).Value;
		SpriteEffects spr = (SpriteEffects)(base.Projectile.spriteDirection == -1);
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, lightColor, base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, spr);
		return false;
	}
}
