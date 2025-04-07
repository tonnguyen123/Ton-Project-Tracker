export interface Task{
    id: number;
    title: string;
    description?: string;
    completed: boolean;
    projectId: number;
}

export interface Project{
    id: number;
    name: string;
    description?: string;
    completed: boolean;
    tasks: Task[];
}