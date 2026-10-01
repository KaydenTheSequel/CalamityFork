using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class FeatherKnifeProjectile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/FeatherKnife";

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.friendly = true;
		base.Projectile.aiStyle = 2;
		base.Projectile.timeLeft = 600;
		base.AIType = 48;
		base.Projectile.DamageType = RogueDamageClass.Instance;
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

	public override void AI()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft % 15 == 0 && base.Projectile.owner == Main.myPlayer)
		{
			int proj = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.position, new Vector2(base.Projectile.velocity.X / 20f, 2f), ModContent.ProjectileType<StickyFeatherAero>(), (int)((double)base.Projectile.damage * 0.4), base.Projectile.knockBack, base.Projectile.owner);
			if (proj.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[proj].DamageType = RogueDamageClass.Instance;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		int proj = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.position, new Vector2(base.Projectile.velocity.X / 20f, 2f), ModContent.ProjectileType<StickyFeatherAero>(), (int)((double)base.Projectile.damage * 0.69), base.Projectile.knockBack, base.Projectile.owner);
		if (proj.WithinBounds(Main.maxProjectiles))
		{
			Main.projectile[proj].DamageType = RogueDamageClass.Instance;
		}
	}
}
