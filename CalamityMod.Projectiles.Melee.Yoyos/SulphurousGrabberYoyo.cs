using System;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Weapons.Melee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee.Yoyos;

public class SulphurousGrabberYoyo : ModProjectile
{
	private int bubbleCounter;

	private bool bubbleStronk;

	private int bubbleStronkCounter;

	private float arbitraryTimer;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<SulphurousGrabber>();

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.YoyosLifeTimeMultiplier[base.Type] = -1f;
		ProjectileID.Sets.YoyosMaximumRange[base.Type] = SulphurousGrabber.Reach;
		ProjectileID.Sets.YoyosTopSpeed[base.Type] = SulphurousGrabber.Speed / 2f;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.aiStyle = 99;
		base.Projectile.width = (base.Projectile.height = 18);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
	}

	public override void AI()
	{
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			if (bubbleStronk)
			{
				base.Projectile.MaxUpdates = 3;
				base.Projectile.localNPCHitCooldown = 10 * base.Projectile.MaxUpdates;
				bubbleStronkCounter++;
			}
			else
			{
				base.Projectile.MaxUpdates = 2;
				base.Projectile.localNPCHitCooldown = 12 * base.Projectile.MaxUpdates;
				bubbleStronkCounter = 0;
			}
			if (bubbleStronkCounter >= 180)
			{
				bubbleStronk = false;
			}
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile proj = enumerator.Current;
				if (proj.type == ModContent.ProjectileType<SulphurousGrabberBubble2>() && proj.ai[0] >= 40f && proj.owner == base.Projectile.owner)
				{
					Rectangle hitbox = base.Projectile.Hitbox;
					if (((Rectangle)(ref hitbox)).Intersects(proj.Hitbox))
					{
						proj.Kill();
						bubbleStronk = true;
						bubbleStronkCounter = 0;
						break;
					}
				}
			}
			arbitraryTimer += (bubbleStronk ? 0.5f : 1f);
			bubbleCounter++;
			if (bubbleCounter >= 60)
			{
				int bubbleAmt = 3;
				for (float i = 0f; i < (float)bubbleAmt; i++)
				{
					int projType = ModContent.ProjectileType<SulphurousGrabberBubble>();
					if (Main.rand.NextBool(8))
					{
						projType = ModContent.ProjectileType<SulphurousGrabberBubble2>();
					}
					float angle = (float)Math.PI * 2f / (float)bubbleAmt * i + (float)Math.Sin(arbitraryTimer / 20f) * ((float)Math.PI / 2f);
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, angle.ToRotationVector2() * 10f, projType, base.Projectile.damage / 2, base.Projectile.knockBack / 4f, base.Projectile.owner);
				}
				bubbleCounter = 0;
			}
		}
		Vector2 val = base.Projectile.position - Main.player[base.Projectile.owner].position;
		if (((Vector2)(ref val)).Length() > 3200f)
		{
			base.Projectile.Kill();
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
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		if (bubbleStronk)
		{
			tex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/Yoyos/SulphurousGrabberYoyoBubble", (AssetRequestMode)2).Value;
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 1, tex);
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Irradiated>(), 120);
	}
}
