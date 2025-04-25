pipeline {
    agent any

    tools {
        dotnet 'dotnet-sdk-6.0'
    }

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
