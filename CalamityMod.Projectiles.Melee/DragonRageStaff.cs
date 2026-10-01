using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.NPCs;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

[PierceResistException(false)]
public class DragonRageStaff : ModProjectile
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<DragonRage>();

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 408);
		base.Projectile.DamageType = TrueMeleeNoSpeedDamageClass.Instance;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 90000;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 255;
		base.Projectile.hide = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 5;
	}

	public override void AI()
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		float spinCycleTime = 50f;
		if (player.dead || !player.channel)
		{
			base.Projectile.Kill();
			player.reuseDelay = 2;
			return;
		}
		int direction = Math.Sign(base.Projectile.velocity.X);
		base.Projectile.velocity = new Vector2((float)direction, 0f);
		if (base.Projectile.ai[0] == 0f)
		{
			base.Projectile.rotation = Utils.ToRotation(new Vector2((float)direction, 0f - player.gravDir)) + MathHelper.ToRadians(135f);
			if (base.Projectile.velocity.X < 0f)
			{
				base.Projectile.rotation -= (float)Math.PI / 2f;
			}
		}
		base.Projectile.ai[0]++;
		base.Projectile.rotation += (float)Math.PI * 4f / spinCycleTime * (float)direction;
		int expectedDirection = (player.SafeDirectionTo(Main.MouseWorld).X > 0f).ToDirectionInt();
		if (base.Projectile.ai[0] % spinCycleTime > spinCycleTime * 0.5f && (float)expectedDirection != base.Projectile.velocity.X)
		{
			player.ChangeDir(expectedDirection);
			base.Projectile.velocity = Vector2.UnitX * (float)expectedDirection;
			base.Projectile.rotation -= (float)Math.PI;
			base.Projectile.netUpdate = true;
		}
		SpawnDust(player, direction);
		PositionAndRotation(player);
		VisibilityAndLight();
	}

	private void SpawnDust(Player player, int direction)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		float rotateDirection = base.Projectile.rotation - (float)Math.PI / 4f * (float)direction;
		Vector2 dustSpawn = base.Projectile.Center + (rotateDirection + ((direction == -1) ? ((float)Math.PI) : 0f)).ToRotationVector2() * 30f;
		Vector2 staffTipDirection = rotateDirection.ToRotationVector2();
		Vector2 tipDustDirection = staffTipDirection.RotatedBy((float)Math.PI / 2f * (float)base.Projectile.spriteDirection);
		if (Main.rand.NextBool())
		{
			Dust dust = Dust.NewDustDirect(dustSpawn - new Vector2(5f), 10, 10, 244, player.velocity.X, player.velocity.Y, 150);
			dust.velocity = base.Projectile.SafeDirectionTo(dust.position) * 0.1f + dust.velocity * 0.1f;
		}
		for (int j = 0; j < 4; j++)
		{
			float dustVelMult = 1f;
			float dustVelMult2 = 1f;
			switch (j)
			{
			case 1:
				dustVelMult2 = -1f;
				break;
			case 2:
				dustVelMult2 = 1.25f;
				dustVelMult = 0.5f;
				break;
			case 3:
				dustVelMult2 = -1.25f;
				dustVelMult = 0.5f;
				break;
			}
			if (!Main.rand.NextBool(6))
			{
				Dust dust2 = Dust.NewDustDirect(base.Projectile.position, 0, 0, 244, 0f, 0f, 100);
				dust2.position = base.Projectile.Center + staffTipDirection * (60f + Main.rand.NextFloat() * 20f) * dustVelMult2;
				dust2.velocity = tipDustDirection * (4f + 4f * Main.rand.NextFloat()) * dustVelMult2 * dustVelMult;
				dust2.noGravity = true;
				dust2.noLight = true;
				dust2.scale = 0.5f;
				if (Main.rand.NextBool(4))
				{
					dust2.noGravity = false;
				}
			}
		}
	}

	private void PositionAndRotation(Player player)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		Vector2 plrCtr = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		Vector2 offset = Vector2.Zero;
		base.Projectile.Center = plrCtr + offset;
		base.Projectile.spriteDirection = base.Projectile.direction;
		base.Projectile.timeLeft = 2;
		player.ChangeDir(base.Projectile.direction);
		player.heldProj = base.Projectile.whoAmI;
		player.itemTime = (player.itemAnimation = 2);
		player.itemRotation = MathHelper.WrapAngle(base.Projectile.rotation);
	}

	private void VisibilityAndLight()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 1.45f, 1.22f, 0.58f);
		base.Projectile.alpha -= 128;
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<Dragonfire>(), 180);
		OnHitEffects(target.Center);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		OnHitEffects(target.Center);
	}

	private void OnHitEffects(Vector2 position)
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			CalamityPlayer modPlayer = Main.player[base.Projectile.owner].Calamity();
			modPlayer.dragonRageHits++;
			if (modPlayer.dragonRageHits >= 10 && modPlayer.dragonRageCooldown <= 0)
			{
				SpawnFireballs();
				modPlayer.dragonRageHits = 0;
			}
			int proj = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), position, Vector2.Zero, ModContent.ProjectileType<FuckYou>(), base.Projectile.damage / 4, base.Projectile.knockBack, base.Projectile.owner, 0f, 0.85f + Main.rand.NextFloat() * 1.15f);
			Main.projectile[proj].DamageType = DamageClass.Melee;
		}
	}

	private void SpawnFireballs()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		int fireballAmt = Main.rand.Next(10, 16);
		Vector2 velocity = default(Vector2);
		for (int i = 0; i < fireballAmt; i++)
		{
			float angleStep = (float)Math.PI * 2f / (float)fireballAmt;
			float speed = 20f;
			((Vector2)(ref velocity))._002Ector(0f, speed);
			velocity = velocity.RotatedBy(angleStep * (float)i * Main.rand.NextFloat(0.9f, 1.1f));
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<DragonRageFireball>(), base.Projectile.damage / 8, base.Projectile.knockBack / 3f, base.Projectile.owner);
		}
		Main.player[base.Projectile.owner].Calamity().dragonRageCooldown = 60;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		Rectangle myRect = base.Projectile.Hitbox;
		if (base.Projectile.owner != Main.myPlayer)
		{
			return;
		}
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC npc = enumerator.Current;
			bool voodooDolls = base.Projectile.owner < 255 && ((npc.type == 22 && player.killGuide) || (npc.type == 54 && player.killClothier));
			bool friendlyProjs = base.Projectile.friendly && (!npc.friendly | voodooDolls);
			bool hostileProjs = base.Projectile.hostile && npc.friendly && !npc.dontTakeDamageFromHostiles;
			if (!npc.dontTakeDamage && (friendlyProjs | hostileProjs) && (base.Projectile.owner < 0 || npc.immune[base.Projectile.owner] == 0 || base.Projectile.maxPenetrate == 1) && (npc.noTileCollide || !base.Projectile.ownerHitCheck || ProjectileLoader.CanHitNPC(base.Projectile, npc) == true))
			{
				Rectangle rect = npc.Hitbox;
				bool canHit;
				if (npc.type == 414)
				{
					int offset = 8;
					rect.X -= offset;
					rect.Y -= offset;
					rect.Width += offset * 2;
					rect.Height += offset * 2;
					canHit = base.Projectile.Colliding(myRect, rect);
				}
				else
				{
					canHit = base.Projectile.Colliding(myRect, rect);
				}
				if (canHit)
				{
					modifiers.HitDirectionOverride = ((player.Center.X < npc.Center.X) ? 1 : (-1));
				}
			}
		}
	}

	public override void CutTiles()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		float staffRadius = 60f;
		float spinning = base.Projectile.rotation - (float)Math.PI / 4f * (float)Math.Sign(base.Projectile.velocity.X);
		DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
		Utils.PlotTileLine(base.Projectile.Center + spinning.ToRotationVector2() * (0f - staffRadius), base.Projectile.Center + spinning.ToRotationVector2() * staffRadius, (float)base.Projectile.width * base.Projectile.scale, DelegateMethods.CutTiles);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		if (((Rectangle)(ref projHitbox)).Intersects(targetHitbox))
		{
			return true;
		}
		float spinning = base.Projectile.rotation - (float)Math.PI / 4f * (float)Math.Sign(base.Projectile.velocity.X);
		float staffRadiusHit = 110f;
		float useless = 0f;
		if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center + spinning.ToRotationVector2() * (0f - staffRadiusHit), base.Projectile.Center + spinning.ToRotationVector2() * staffRadiusHit, 23f * base.Projectile.scale, ref useless))
		{
			return true;
		}
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPos = base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY);
		Rectangle rectangle = default(Rectangle);
		((Rectangle)(ref rectangle))._002Ector(0, 0, tex.Width, tex.Height);
		Vector2 origin = tex.Size() / 2f;
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Main.EntitySpriteDraw(tex, drawPos, rectangle, lightColor, base.Projectile.rotation, origin, base.Projectile.scale, spriteEffects);
		return false;
	}
}
