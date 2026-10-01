using System;
using System.IO;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Utilities.Daybreak;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class SarosAura : ModProjectile, ILocalizedModType, IModType
{
	public static Asset<Texture2D> circle;

	public new string LocalizationCategory => "Projectiles.Summon";

	public int MinionSlotsToAdd
	{
		get
		{
			return (int)base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 50;
		base.Projectile.height = 50;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 1f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		player.AddBuff(ModContent.BuffType<SarosPossessionBuff>(), 3600);
		if (MinionSlotsToAdd > 0)
		{
			float minionSlotsAvaliable = player.maxMinions;
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile item = enumerator.Current;
				if (item.owner == base.Projectile.owner)
				{
					minionSlotsAvaliable -= item.minionSlots;
				}
			}
			while (minionSlotsAvaliable >= 1f && MinionSlotsToAdd > 0)
			{
				base.Projectile.minionSlots++;
				minionSlotsAvaliable--;
				MinionSlotsToAdd--;
				player.channel = false;
				base.Projectile.netUpdate = true;
			}
			if (MinionSlotsToAdd > 0)
			{
				Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<SarosEclipseBeam>(), base.Projectile.damage * (int)base.Projectile.minionSlots, base.Projectile.knockBack, base.Projectile.owner);
			}
			MinionSlotsToAdd = 0;
		}
		if (base.Projectile.type == ModContent.ProjectileType<SarosAura>())
		{
			if (player.dead)
			{
				modPlayer.sunSpirit = false;
			}
			if (modPlayer.saros)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		base.Projectile.Center = player.Center + Vector2.UnitY * (player.gfxOffY + player.gravDir * -24f);
		Lighting.AddLight(base.Projectile.Center, (float)(255 - base.Projectile.alpha) * 0.25f / 255f, (float)(255 - base.Projectile.alpha) * 0.25f / 255f, (float)(255 - base.Projectile.alpha) * 0f / 255f);
		if (GetTargetInRange(1600f) == null || base.Projectile.owner != Main.myPlayer)
		{
			return;
		}
		if (base.Projectile.ai[1] > 0f)
		{
			base.Projectile.ai[1]--;
		}
		else if (player.ownedProjectileCounts[ModContent.ProjectileType<SarosEclipseBeam>()] <= 0)
		{
			int damage = (int)((float)base.Projectile.damage * (0.8f + base.Projectile.minionSlots * 0.2f));
			float shootSpeed = 15f;
			_ = base.Projectile.Center;
			for (int i = 0; i < 3; i++)
			{
				SoundEngine.PlaySound(SarosPossession.FiringSound, base.Projectile.Center);
				Vector2 velocity = Vector2.UnitX.RotatedByRandom(6.2831854820251465) * shootSpeed;
				Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center - velocity, velocity, ModContent.ProjectileType<SarosSunfire>(), damage, base.Projectile.knockBack, base.Projectile.owner, 120f, (base.Projectile.minionSlots - 1f) / 9f).DamageType = DamageClass.Summon;
			}
			base.Projectile.ai[1] += 60f / (0.8f + base.Projectile.minionSlots * 0.2f);
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.minionSlots);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.minionSlots = reader.ReadSingle();
	}

	private NPC GetTargetInRange(float range)
	{
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (player.HasMinionAttackTargetNPC && Main.npc[player.MinionAttackTargetNPC].CanBeChasedBy() && base.Projectile.IsInRangeOfMeOrMyOwner(Main.npc[player.MinionAttackTargetNPC], range, out var _, out var _, out var _))
		{
			return Main.npc[player.MinionAttackTargetNPC];
		}
		NPC gotTarget = null;
		float currentDistance = range;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC npc = enumerator.Current;
			float myDistance2 = npc.Distance(base.Projectile.Center);
			if (npc.CanBeChasedBy() && myDistance2 < currentDistance)
			{
				currentDistance = myDistance2;
				gotTarget = npc;
			}
		}
		return gotTarget;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		Texture2D spTex = TextureAssets.Projectile[base.Type].Value;
		Main.spriteBatch.Draw(spTex, base.Projectile.Center - Main.screenPosition, (Rectangle?)null, Color.White, Main.GlobalTimeWrappedHourly, spTex.Size() * 0.5f, 0.75f, (SpriteEffects)0, 0f);
		using (Main.spriteBatch.Scope())
		{
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.Additive, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			int count = (int)((base.Projectile.minionSlots - 1f) / 3f) * 2 + 2;
			for (int i = 0; i < count; i++)
			{
				float comp = (float)i / (float)count;
				float offset = (float)(Main.mouseTextColor - 190) / 64f * 8f;
				if (i % 2 == 0)
				{
					offset = 8f - offset;
				}
				Main.spriteBatch.DrawLineBetter(base.Projectile.Center + Utils.RotatedBy(new Vector2(20f + offset, 0f), (double)((float)Math.PI * 2f * comp - Main.GlobalTimeWrappedHourly), default(Vector2)), base.Projectile.Center + Utils.RotatedBy(new Vector2(34f + offset, 0f), (double)((float)Math.PI * 2f * comp - Main.GlobalTimeWrappedHourly), default(Vector2)), (i % 2 == 3) ? Color.OrangeRed : Color.Gold, 2f);
			}
			Texture2D ciTex = CalamityUtils.GetTextureEfficient(ref circle, "CalamityMod/Particles/BloomRingThinLarge").Value;
			count = (int)MathHelper.Min(1f + (base.Projectile.minionSlots - 1f) % 3f, 10f);
			for (int j = 0; j < count && j < 5; j++)
			{
				_ = j / count;
				float offset2 = (float)(Main.mouseTextColor - 190) / 64f;
				if (j % 2 == 0)
				{
					offset2 = 1f - offset2;
				}
				Main.EntitySpriteDraw(ciTex, base.Projectile.Center - Main.screenPosition, null, Color.Lerp(Color.Gold, Color.OrangeRed, (float)j / 4f), Main.GlobalTimeWrappedHourly, ciTex.Size() * 0.5f, 0.0225f + 0.0025f * offset2 + 0.005f * (float)j, (SpriteEffects)0);
			}
			Main.spriteBatch.End();
		}
		return false;
	}
}
