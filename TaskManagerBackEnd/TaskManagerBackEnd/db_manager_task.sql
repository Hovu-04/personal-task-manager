Create database task_manager_db;

-- Tạo ENUM types
CREATE TYPE task_priority AS ENUM ('low', 'medium', 'high');
CREATE TYPE task_status AS ENUM ('todo', 'in_progress', 'done', 'archived');

-- Bảng users
CREATE TABLE users
(
    id            SERIAL PRIMARY KEY,
    full_name     VARCHAR(100)        NOT NULL,
    email         VARCHAR(255) UNIQUE NOT NULL,
    password_hash TEXT                NOT NULL,
    role          VARCHAR(50) DEFAULT 'user',
    last_login    TIMESTAMP,
    created_at    TIMESTAMP   DEFAULT NOW()
);

-- Bảng categories (mỗi user có danh mục riêng)
CREATE TABLE categories
(
    id      SERIAL PRIMARY KEY,
    user_id INT          NOT NULL REFERENCES users (id) ON DELETE CASCADE,
    name    VARCHAR(100) NOT NULL,
    UNIQUE (user_id, name) -- Đảm bảo tên category không trùng trong cùng user
);

-- Bảng tasks
CREATE TABLE tasks
(
    id          SERIAL PRIMARY KEY,
    user_id     INT           NOT NULL REFERENCES users (id) ON DELETE CASCADE,
    category_id INT           REFERENCES categories (id) ON DELETE SET NULL,
    title       VARCHAR(255)  NOT NULL,
    description TEXT,
    priority    task_priority NOT NULL,
    due_date    TIMESTAMP     NOT NULL,
    status      task_status DEFAULT 'todo',
    created_at  TIMESTAMP   DEFAULT NOW()
);

-- Bảng reminders
CREATE TABLE reminders
(
    id            SERIAL PRIMARY KEY,
    task_id       INT       NOT NULL REFERENCES tasks (id) ON DELETE CASCADE,
    reminder_time TIMESTAMP NOT NULL,
    is_sent       BOOLEAN DEFAULT FALSE,
    sent_at       TIMESTAMP
);

-- Tạo indexes để tối ưu query
CREATE INDEX idx_tasks_user_id ON tasks (user_id);
CREATE INDEX idx_tasks_due_date ON tasks (due_date);
CREATE INDEX idx_tasks_status ON tasks (status);


select *
from public."users";
