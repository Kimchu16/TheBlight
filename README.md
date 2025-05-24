# 🧠 Unity + GitHub Workflow Guide

Welcome to the project! Here's how to work safely and cleanly with Unity + GitHub so we avoid merge conflicts, .meta file issues, and broken scenes.

---

## ✅ Getting Started

### 1. Clone the Repo (Only Once)
- Open GitHub Desktop
- File > Clone Repository
- Choose this repo and clone it to your local machine

> **⚠️ Do NOT drag and drop files or download the ZIP. Always use GitHub Desktop.**

---

## 🧱 Project Setup Rules

- **Unity Version**: `6000.0.41f1`
- Do not change Unity version unless agreed
- Make sure `.gitignore` and `.gitattributes` are respected

---

## 🌿 Branch Workflow

### Always work in your own branch!

1. Start from `main`
   ```bash
   git checkout main
   git pull origin main
   ```

2. Create a new branch
   ```bash
   git checkout -b feature/my-feature-name
   ```

3. Work on your feature in Unity

4. Commit and push your changes
   ```bash
   git add .
   git commit -m "Add new feature"
   git push origin feature/my-feature-name
   ```

---

## 🧩 Working with Scenes & Prefabs

### Scenes
- **NEVER edit the main gameplay scene unless you're assigned.**
- Use your own dev scene **DON'T WORK ON THE SAME SCENE AT THE SAME TIME**:
  ```
  Dev_YourNameScene.unity
  ```

### Prefabs
- Use nested prefabs
- Only modify prefabs you're assigned
- Don’t rename or move assets in file explorer, do it in Unity

---

## 💡 Pulling Latest Changes

### If you're on a feature branch and want the latest main changes:

1. Make sure you've committed your work.
2. Then pull updates:
   ```bash
   git checkout main
   git pull origin main
   git checkout feature/my-feature
   git merge main
   ```

---

## 🧼 Before Opening a PR

- Test your scene and prefabs
- Check for .meta file issues or unrelated changes
- Add a descriptive commit message and PR summary

---

## ⚠️ Common Mistakes to Avoid

- ❌ Don’t work directly in `main`
- ❌ Don’t push broken scenes
- ❌ Don’t rename or delete shared files without telling the team
- ✅ Do pull from `main` before starting new work
- ✅ Do use GitHub Desktop or Terminal — no GUI-only Unity workflows

---

## 🛠 Quick Commands Reference

```bash
# Clone the repo (first time only)
git clone <repo-url>

# Create and switch to a new feature branch
git checkout -b feature/your-feature

# Add and commit your work
git add .
git commit -m "Your message"

# Push to GitHub
git push origin feature/your-feature

# Merge main into your branch
git checkout main
git pull origin main
git checkout feature/your-feature
git merge main
```

---

## 🙋 Need Help?

If you’re unsure about something then ask. Better safe than conflicted.

```
Team Motto: “Don’t break main. Don’t touch what you don’t own.” 💀
```
