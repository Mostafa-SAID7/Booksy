# Booksy API Release Process

This document explains how releases work for Booksy API, including versioning, tagging, and deployment.

## Table of Contents

- [Overview](#overview)
- [Semantic Versioning](#semantic-versioning)
- [Automatic Release Process](#automatic-release-process)
- [Release Lifecycle](#release-lifecycle)
- [Version Calculation](#version-calculation)
- [Release Types](#release-types)
- [GitHub Releases](#github-releases)
- [Rollback](#rollback)
- [Troubleshooting](#troubleshooting)

---

## Overview

Booksy API uses **semantic versioning** combined with **conventional commits** to automatically calculate and publish releases.

**Key Principle**: No manual version bumping. Versions are calculated from git history.

### How It Works

```
Developer writes conventional commits
        ↓
Commits pushed to master
        ↓
semantic-release workflow triggered
        ↓
Analyze commit types (feat, fix, etc.)
        ↓
Calculate next version (MAJOR.MINOR.PATCH)
        ↓
Generate release notes from commit messages
        ↓
Update CHANGELOG.md
        ↓
Create git tag (v1.2.3)
        ↓
Create GitHub Release
        ↓
Build & push Docker image (ghcr.io/...:1.2.3)
```

---

## Semantic Versioning

Booksy API follows [Semantic Versioning 2.0.0](https://semver.org/).

### Format

```
MAJOR.MINOR.PATCH[-prerelease][+build]
```

Examples: `1.0.0`, `1.2.3`, `2.0.0-beta.1`, `1.0.0+20260907`

### Version Bumping Rules

| Commit Type | Example | Version Change |
|-------------|---------|-----------------|
| `fix:` | fix(auth): prevent token reuse | 1.0.0 → 1.0.1 |
| `feat:` | feat(cart): add persistence | 1.0.0 → 1.1.0 |
| `feat:` with `BREAKING` | feat!: redesign auth | 1.0.0 → 2.0.0 |

### BREAKING CHANGE

A commit introduces a breaking change when:

1. **Explicit footer**: Message includes `BREAKING CHANGE:` footer

```
feat(api)!: redesign authentication contract

BREAKING CHANGE: /auth/login now requires 2FA
```

2. **Exclamation mark**: Type followed by `!`

```
feat!: remove deprecated /v1 endpoints
```

Both trigger a MAJOR version bump.

---

## Automatic Release Process

### When Do Releases Happen?

Releases are **automatically created** when:

1. ✅ Commits are pushed to `master` branch
2. ✅ Commits follow [Conventional Commits](CONTRIBUTING.md#conventional-commits)
3. ✅ Commits contain `feat:`, `fix:`, `perf:`, or `revert:` types
4. ✅ Commit message does NOT contain `[skip ci]` or `chore(release):`
5. ✅ All CI checks pass (build, tests, security, docker)

### When NO Release Happens

Releases are **skipped** for:

- ❌ Documentation changes (`docs:`)
- ❌ Formatting changes (`style:`)
- ❌ Test-only changes (`test:`)
- ❌ Internal refactoring (`refactor:`)
- ❌ Build system changes (`build:`)
- ❌ CI/CD changes (`ci:`)
- ❌ Maintenance (`chore:`)

### Release Workflow

Located at: `.github/workflows/release.yml`

**Trigger**: Push to `master` branch  
**Timeout**: 15 minutes  
**Concurrency**: Single job (prevents duplicate releases)

---

## Release Lifecycle

### Step 1: Commit Analysis

The release workflow reads the git log and analyzes commits since the last release tag.

```
git log v1.0.0..master --pretty=format:"%H %s"
```

Commits are categorized:
- `feat:` → MINOR bump
- `fix:`, `perf:` → PATCH bump
- `BREAKING CHANGE:` → MAJOR bump

### Step 2: Version Calculation

```
Previous: v1.0.0
New commits: feat(X), fix(Y)
Next version: v1.1.0 (MINOR + PATCH = MINOR)
```

Calculation is **deterministic**: same commits always produce same version.

### Step 3: Changelog Generation

Release notes are generated from commit messages:

```markdown
## ✨ Features
- feat(cart): add persistence

## 🐛 Bug Fixes
- fix(auth): prevent token reuse

## 🔧 Refactoring
- refactor(db): optimize queries
```

Appended to `CHANGELOG.md`.

### Step 4: Git Tag Creation

Create annotated tag:

```bash
git tag -a v1.1.0 -m "Release v1.1.0"
git push origin v1.1.0
```

Tag points to exact merge commit.

### Step 5: GitHub Release

Create GitHub Release with:
- **Title**: `v1.1.0`
- **Description**: Generated release notes
- **Assets**: Docker image reference (not uploaded, just mentioned)

### Step 6: Docker Image

The `docker.yml` workflow is triggered by tag:

```
docker build -t ghcr.io/YOUR_ORG/booksy-api:1.1.0 .
docker push ghcr.io/YOUR_ORG/booksy-api:1.1.0
```

Image is tagged with:
- Semantic version: `1.1.0`
- Major.minor: `1.1`
- Latest: `latest` (on master only)

---

## Release Types

### Regular Release

Normal release with features/fixes:

```
v1.0.0 → v1.1.0 (feature)
v1.1.0 → v1.1.1 (bugfix)
```

### Major Release (Breaking Change)

When a breaking change is introduced:

```
v1.9.9 → v2.0.0

BREAKING CHANGE: deprecated endpoints removed
- /v1/books → use /v2/books
- LoginRequest.username → use email
```

**Developer Responsibility**: Update documentation and provide migration guide.

### Prerelease (Not Implemented)

Typically used for release candidates:

```
v2.0.0-rc.1
v2.0.0-rc.2
v2.0.0
```

Current setup uses stable releases only on `master`.

---

## GitHub Releases

### Where to Find

Repository → Releases → [Latest]

### What's Included

Each GitHub Release contains:

1. **Version**: `v1.2.3`
2. **Release Date**: Automatically set
3. **Release Notes**: Auto-generated from commits
4. **Assets**: None (Docker image is in container registry)
5. **Source Code**: ZIP and TAR archives (GitHub-generated)

### Release Notes Format

```markdown
# Booksy API v1.1.0

Released on September 7, 2026

## ✨ Features
- feat(cart): add item persistence to Redis
- feat(search): implement full-text search

## 🐛 Bug Fixes
- fix(auth): prevent expired token reuse
- fix(api): correct pagination offset calculation

## ⚡ Performance
- perf(db): optimize product search (2.5s → 150ms)

## 📚 Documentation
- docs: update auth migration guide

---

**Commits**: v1.0.0...v1.1.0  
**Contributors**: 3 developers
```

---

## Version Calculation Examples

### Example 1: Feature + Bugfix

```
Previous version: v1.0.0

Commits since v1.0.0:
  feat(cart): add persistence        ← MINOR
  fix(auth): token validation        ← PATCH
  docs: update README                ← NO BUMP

Next version: v1.1.0 (MINOR takes precedence)
```

### Example 2: Breaking Change

```
Previous version: v1.5.2

Commits since v1.5.2:
  feat(api)!: redesign endpoints     ← MAJOR
  fix(db): connection leak           ← PATCH

Next version: v2.0.0 (MAJOR takes precedence)
```

### Example 3: No Release

```
Previous version: v1.0.0

Commits since v1.0.0:
  chore: update dependencies         ← NO BUMP
  docs: add examples                 ← NO BUMP
  refactor(service): improve clarity ← NO BUMP

Next version: NONE (no release created)
```

---

## Rollback

### Scenario: Released Version Has Critical Bug

#### Option 1: Hotfix (Recommended)

```bash
# Create fix from the release tag
git checkout v1.1.0
git checkout -b hotfix/critical-bug

# Make fix
# Commit with conventional format
git commit -m "fix: critical security issue"

# Create PR targeting master
git push origin hotfix/critical-bug
```

New version created: `v1.1.1`

#### Option 2: Revert Previous Release

```bash
git revert v1.1.0
git push origin master
```

Creates a **new commit** that undoes changes. CI will create `v1.1.2` with reverted code.

**Never delete release tags** or rewrite git history.

### Prevent Release Temporarily

If you need to skip release for a commit:

```bash
git commit -m "chore: update dependencies [skip ci]"
```

Use `[skip ci]` to prevent CI/CD workflow from running.

---

## Troubleshooting

### Release Not Created

**Check list:**

1. ✓ Commits follow [Conventional Commits](CONTRIBUTING.md#conventional-commits)?
   - Must have `feat:`, `fix:`, `perf:`, or `revert:` prefix

2. ✓ All CI checks passed?
   - Build, tests, security, docker must all pass

3. ✓ Pushed to `master` branch?
   - Other branches don't trigger releases

4. ✓ Commit message doesn't contain `[skip ci]`?

5. ✓ No pre-existing tag for this version?

**Debug:**

```bash
# Check GitHub Actions
# Repository → Actions → Release workflow

# Check git log
git log --oneline -20 --grep="feat\|fix"

# Check existing tags
git tag
```

### Release with Wrong Version

**This shouldn't happen** (deterministic calculation).

If it does:

1. ✗ Delete the tag: `git tag -d v1.2.3` and `git push origin :v1.2.3`
2. ✗ Create PR to fix the commit message
3. ✓ Push to master with correct commit
4. ✓ Release will calculate correct version

### Release Notes Incorrect

Edit GitHub Release description manually if needed:

Repository → Releases → [Version] → Edit

Changes are preserved locally but won't sync to CHANGELOG.md.

### Docker Image Not Pushed

**Check:**

1. ✓ Docker credentials configured in GitHub Secrets?
   - `GITHUB_TOKEN` should be available automatically

2. ✓ Branch is `master` or tag matches `v*`?

3. ✓ Container registry is accessible?

**Debug:**

GitHub Actions → docker.yml → Logs

---

## Release Checklist for Team

### Before Merging PR to Master

- [ ] All commits follow [Conventional Commits](CONTRIBUTING.md#conventional-commits)
- [ ] PR title is concise and clear
- [ ] All CI checks pass (green checkmarks)
- [ ] Code reviewed and approved
- [ ] Security warnings addressed
- [ ] Database migrations (if any) tested
- [ ] Documentation updated (README, SECURITY, etc.)

### After Merge to Master

- [ ] GitHub Actions Release workflow triggered automatically
- [ ] Release appears in GitHub Releases
- [ ] CHANGELOG.md updated with new version
- [ ] Docker image pushed to container registry
- [ ] Slack/email notification sent (if configured)

### If Deployment Required

- [ ] Pull latest release tag: `git pull origin --tags`
- [ ] Deploy Docker image: `docker pull ghcr.io/YOUR_ORG/booksy-api:1.2.3`
- [ ] Run database migrations (if any)
- [ ] Verify health check endpoint
- [ ] Monitor logs for errors

---

## Security Note

**Never manually edit CHANGELOG.md or git tags.**

These are automatically managed by semantic-release. Manual edits can cause:

- Duplicate releases
- Incorrect version numbers
- Lost release history

If you need to update release notes after publication, edit the GitHub Release directly (does not affect CHANGELOG.md).

---

## FAQ

**Q: Can I manually trigger a release?**  
A: No. Releases are automatic based on commits. Push your code to master with conventional commits.

**Q: Can I bump MAJOR version manually?**  
A: Add `BREAKING CHANGE:` footer to any commit. Commits are read-only—cannot be edited after push.

**Q: What if I made a mistake in my commit message?**  
A: Create a new commit that fixes it. Example: `feat: add feature` (typo) → new commit `fix: typo in feature description`.

**Q: Can I release multiple versions at once?**  
A: No. Each push to master triggers max one release, based on all commits since last tag.

**Q: How do I see what version will be released next?**  
A: Check the `release.yml` workflow dry-run output in GitHub Actions.

---

## Reference

- [Semantic Versioning](https://semver.org/)
- [Conventional Commits](https://www.conventionalcommits.org/)
- [Keep a Changelog](https://keepachangelog.com/)
- [semantic-release Documentation](https://github.com/semantic-release/semantic-release)

---

**Last Updated**: September 7, 2026  
**Version**: 1.0.0  
**Status**: Production Ready ✅
