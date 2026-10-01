using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class AtlasMunitionsDropPodUpper : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 82;
		base.Projectile.height = 80;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 240;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		base.Projectile.velocity.Y = MathHelper.Clamp(base.Projectile.velocity.Y - 0.2f, -29f, 0f);
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 6 % Main.projFrames[base.Type];
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/AtlasMunitionsDropPodUpperGlow", (AssetRequestMode)2).Value;
		Rectangle frame = value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Main.EntitySpriteDraw(value, drawPosition, frame, base.Projectile.GetAlpha(Color.White), base.Projectile.rotation, frame.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
	}
}
