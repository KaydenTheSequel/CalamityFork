using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class SubductionSlicerProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/SubductionSlicer";

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 5;
		base.Projectile.aiStyle = 3;
		base.Projectile.extraUpdates = 1;
		base.Projectile.timeLeft = 360;
		base.Projectile.alpha = 55;
		base.AIType = 52;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 15;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.25f, 0.15f, 0f);
		if (Main.rand.NextBool(5))
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, Main.rand.NextBool(3) ? 16 : 127, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f);
		}
		Vector2 goreVec = default(Vector2);
		((Vector2)(ref goreVec))._002Ector(base.Projectile.position.X + (float)(base.Projectile.width / 2) + base.Projectile.velocity.X, base.Projectile.position.Y + (float)(base.Projectile.height / 2) + base.Projectile.velocity.Y);
		if (Main.rand.NextBool(8) && !Main.dedServ)
		{
			int smoke = Gore.NewGore(base.Projectile.GetSource_FromAI(), goreVec, default(Vector2), Main.rand.Next(375, 378), 0.75f);
			Main.gore[smoke].behindTiles = true;
		}
		if (base.Projectile.localAI[0] > 0f)
		{
			base.Projectile.localAI[0]--;
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

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(323, 240);
		OnHitEffects(target.Center);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(323, 240);
		OnHitEffects(target.Center);
	}

	private void OnHitEffects(Vector2 position)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			int proj = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), position, Vector2.Zero, ModContent.ProjectileType<FuckYou>(), (int)((float)base.Projectile.damage * 0.8f), base.Projectile.knockBack, base.Projectile.owner, 0f, 0.85f + Main.rand.NextFloat() * 1.15f);
			if (proj.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[proj].DamageType = RogueDamageClass.Instance;
			}
			if (base.Projectile.Calamity().stealthStrike && base.Projectile.localAI[0] <= 0f)
			{
				Vector2 spawnPos = default(Vector2);
				((Vector2)(ref spawnPos))._002Ector(position.X, position.Y + 30f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spawnPos, Vector2.Zero, ModContent.ProjectileType<SubductionFlameburst>(), (int)((float)base.Projectile.damage * 1.2f), 2f, base.Projectile.owner, 1f);
				base.Projectile.localAI[0] = 300f;
			}
		}
	}
}
