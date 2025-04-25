pipeline {
    agent any

    stages {
        stage('Restore') {
            steps {
                bat 'dotnet restore'
            }
        }
        stage('Build') {
            steps {
                bat 'dotnet build --configuration Release'
            }
        }
        stage('Test') {
            steps {
                bat 'dotnet test'
            }
        }
    }
}
